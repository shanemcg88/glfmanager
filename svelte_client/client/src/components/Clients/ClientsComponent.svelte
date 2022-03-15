<script>
    import ContentTable from '../tables/ContentTable.svelte';
    import { clientList } from '../../stores';
    import { clientTableData } from './clientTableData';

    let clients;
    clientList.subscribe(() => clients = clientList);
</script>

{ #if $clients.length > 0 }
    <ContentTable
        tableContent = { clients }
        tableSettings = { clientTableData }
    />
{ /if }