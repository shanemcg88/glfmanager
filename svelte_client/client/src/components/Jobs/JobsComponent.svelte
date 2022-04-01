<script>
    import ContentTable from '../tables/ContentTable.svelte';
    import { jobsList } from '../../stores';
    import { jobsTableData } from './jobsTableData';
    import { onDestroy } from 'svelte';


    let jobs;
    jobsList.subscribe(() => jobs = jobsList);
</script>

{ #if $jobs.length > 0 }

    <ContentTable
        tableContent = { jobs }
        tableSettings = { jobsTableData }
    />
{ :else }
    <h4>No Jobs Found</h4>
{ /if }

