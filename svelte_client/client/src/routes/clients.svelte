<script>
    import { getClients } from '../shared/globalFetch.svelte';
    import ClientsComponent from '../components/Clients/ClientsComponent.svelte';
</script>

<h1>Clients</h1>

{#await getClients()}
    <p>Getting clients...</p>
{:then}
    <ClientsComponent />
{:catch error}
    <h4>{ error }</h4>
{/await}