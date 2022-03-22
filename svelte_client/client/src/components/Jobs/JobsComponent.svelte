<script>
    import ContentTable from '../tables/ContentTable.svelte';
    import { jobsList } from '../../stores';
    import { jobsTableData } from './jobsTableData';
    console.log('jobslist', jobsList);
    let jobs;
    let employeeString = '';
    jobsList.subscribe((x) => jobs = jobsList);
    // jobsList.subscribe((res) => {
    //     console.log('res', res);
    //     // Converting an employee list to a string for the content table
    //     var emps = res[0].employeeList;

    //     for (let i=0; i < emps.length-1; i++) {
    //         if (i === emps.length-1 || emps.length <= 1) {
    //             employeeString += emps[i]['firstName'] + ' ' + emps[i]['lastName'];
    //         } else { 
    //             employeeString += emps[i]['firstName'] + ' ' + emps[i]['lastName'] + ', '; 
    //         }
    //     }

    //     res[0].employeeList = employeeString;
    //     this.jobs = res;
    // })
    console.log('hi')
    $: console.log('JOBS', $jobs);
</script>

{ #if $jobs.length > 0 }
    <ContentTable
        tableContent = { jobs }
        tableSettings = { jobsTableData }
    />
{ :else }
    <h4>No Jobs Found</h4>
{ /if }

