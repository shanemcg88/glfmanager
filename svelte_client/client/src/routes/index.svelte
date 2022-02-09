<script>
import { goto } from "$app/navigation";
import { onMount } from "svelte";
import { signedIn } from "../auth";
import TopNavBar from "../components/navbar/TopNavBar.svelte";
import SideBarNav from "../components/navbar/SideNavBar.svelte";
import SideNavBar from "../components/navbar/SideNavBar.svelte";

let rootUrl = import.meta.env.VITE_ROOTURL;
let isSignedIn

signedIn.subscribe(value => {isSignedIn = value});

onMount(async () => {
    const response = await fetch(`${rootUrl}/useraccount/auth`, {
            method: 'GET',
            mode: 'cors', 
            credentials: 'include',
        })

    if (response.ok) {
        signedIn.update((value) => value = true);
    }
    else {
        signedIn.update(value => value = false);
        goto('/login')
    }
})

const signOut = async () => {
    const response = await fetch(`${rootUrl}/useraccount/logout`, {
        method: 'GET',
        mode: 'cors',
        credentials: 'include'
    })

    if (response.ok) {
        signedIn.update(value => value = false);
        goto('/login');
    } else {
        throw new Error("Something went wrong logging out");
    }
}

</script>
<svelte:head>
    <title>GLF Manager</title>
</svelte:head>

<TopNavBar />
<SideNavBar />
<div>
</div>
