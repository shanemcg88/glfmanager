<script>
    import ContentTable from '../tables/ContentTable.svelte';
    import { jobsList } from '../../stores';
    import { jobsTableData } from './jobsTableData';
    import { onMount } from 'svelte';
    let jobs;
    let employeeString = '';
    jobsList.subscribe(() => jobs = jobsList);

    function flattenEmployeeNames(jobsFromStore) {
        console.log('jobsFromStore start', jobsFromStore);
        if (jobsFromStore.length === 0)
            return;

        var workers = jobsFromStore[0].employeeList;
        console.log('workers', workers);
        for (let i=0; i < workers.length; i++) {
            console.log('first ran');
            if (i === workers.length-1 || workers.length <= 1) {
                console.log('second ran');
                employeeString += workers[i]['firstName'] + ' ' + workers[i]['lastName'];
            } else { 
                console.log('third ran');
                employeeString += workers[i]['firstName'] + ' ' + workers[i]['lastName'] + ', '; 
            }    
        }
        console.log ('jobsFromStore at end', jobsFromStore);
        return jobsFromStore;
    }

    $: flattenEmployeeNames($jobs);
</script>

{ #if $jobs.length > 0 }
    <ContentTable
        tableContent = { jobs }
        tableSettings = { jobsTableData }
    />
{ :else }
    <h4>No Jobs Found</h4>
{ /if }

