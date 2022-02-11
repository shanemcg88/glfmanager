<script>
import { goto } from "$app/navigation";
import { signedIn } from "../auth";
import TopNavBar from "../components/navbar/TopNavBar.svelte";
import SideNavBar from "../components/navbar/SideNavBar.svelte";

let rootUrl = import.meta.env.VITE_ROOTURL;
let isSignedIn

// get initial value
signedIn.subscribe(value => {isSignedIn = value});

let userAuth = async () => {
    await fetch(`${rootUrl}/useraccount/auth`, {
        method: 'GET',
        mode: 'cors', 
        credentials: 'include',
    }).then(res => {
        if (res.status===401){
            signedIn.update(value => value = false);
            goto('/login')
        }
        if (res.ok)
            res.json();
    }).then(()=>
        signedIn.update((value) => value = true)
    ).catch(() => {
        signedIn.update(value => value = false);
        goto('/login')
    })
}

</script>

<svelte:head>
    <title>GLF Manager</title>
</svelte:head>

{#await userAuth()}
    <p>...loading</p>

{:then _}
    <TopNavBar />
    <SideNavBar />
{/await}