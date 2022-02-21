<script>
import { goto } from "$app/navigation";
import { signedIn } from "../auth";
import { userAuth } from '../shared/globalFetch.svelte';
import { ROOT_URL } from "../stores";
import { navOptions } from "../components/navbar/Nav.svelte";
import TopNavBar from "../components/navbar/TopNavBar.svelte";
import SideNavBar from "../components/navbar/SideNavBar.svelte";
import Jobs from './jobs.svelte'

let rootUrl;
let isSignedIn
let pageSelected = navOptions[0];
let intSelected = 0;

ROOT_URL.subscribe(value => rootUrl = value);
signedIn.subscribe(value => isSignedIn = value);

// Dynamically changing component when user clicks on side navigation bar
function changeComponent(event) {
    pageSelected = navOptions[event.detail.number];
    intSelected = event.detail.number;
}

</script>

<svelte:head>
    <title>GLF Manager</title>
</svelte:head>

{#await userAuth()}
    <p>...loading</p>

{:then}
<div class="container-fluid">
    <div class="row">
        <TopNavBar />
    </div>
    
    <div class="row" id="sideNavContainer">
        <SideNavBar 
            on:changePage={changeComponent}
            {intSelected}
        />

        <div class="col-sm" id="content">
            <svelte:component this={pageSelected.component}/>
        </div>
    </div>
</div>
{/await}

<style>
    .container-fluid {
        width: 98vw;
    }
</style>