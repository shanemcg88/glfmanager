<script>
import SideNavBar from "../components/navbar/SideNavBar.svelte";
import TopNavBar from "../components/navbar/TopNavBar.svelte";
import { signedIn } from '../auth';
import Login from "./login.svelte";
import { userAuth } from "../shared/globalFetch.svelte";
import { navOptions } from "../components/navbar/Nav.svelte";

// import ContentTable from "src/components/tables/ContentTable.svelte";
// import Clients from "./clients.svelte";
let isSignedIn;
let pageSelected = navOptions[0];
let intSelected = 0;

signedIn.subscribe(value => isSignedIn = value);
function changeComponent(event) {
    pageSelected = navOptions[event.detail.number];
    intSelected = event.detail.number;
}

</script>

{#await userAuth()}
    <p>...Loading</p>
{:then}
    <nav>
        <div class="container-fluid">
            <div class="row">
                <TopNavBar />
            </div>
            <div class="row" id="sideNavContainer">
                <SideNavBar on:changePage={changeComponent}
                {intSelected}
                />
                <div class="col-sm" id="content">
                    <!-- <svelte:component this={pageSelected.component}/> -->
                    <slot />
                </div>
            </div>
        </div>
    </nav>

{/await}

<style>
    .container-fluid {
        width: 98vw;
    }
</style>