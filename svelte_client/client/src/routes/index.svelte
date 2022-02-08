<script>
import { goto } from "$app/navigation";
import { onMount } from "svelte";
import { signedIn } from "../auth";

let rootUrl = import.meta.env.VITE_ROOTURL;
let isSignedIn

signedIn.subscribe(value => {isSignedIn = value});

onMount(async () => {
    const response = await fetch(`${rootUrl}/useraccount/auth`, {
            method: 'GET',
            mode: 'cors', 
            credentials: 'include',
        })

    if (response.ok)
        signedIn.update((value) => value = true);
    else
        signedIn.update(value => value = false);
})

const signOut = async () => {
    const response = await fetch(`${rootUrl}/useraccount/logout`, {
        method: 'GET',
        mode: 'cors',
        credentials: 'include'
    })

    if (response.ok) {
        signedIn.update(value => value = false);
    } else {
        throw new Error("Something went wrong logging out");
    }
}

</script>
<svelte:head>
    <title>GLF Manager</title>
</svelte:head>

<main>
    <h1>GLF Manager</h1>
</main>
<nav>
    {#if isSignedIn}
        <button type="button" on:click={signOut}>Logout</button>
    {:else}
        <button type="button" on:click={()=>goto('/login')}>Login</button>
    {/if}


</nav>
