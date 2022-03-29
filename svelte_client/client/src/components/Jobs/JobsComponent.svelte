<script>
    import ContentTable from '../tables/ContentTable.svelte';
    import { jobsList } from '../../stores';
    import { jobsTableData } from './jobsTableData';

    let jobs
    jobsList.subscribe(() => jobs = jobsList);
    // function unsub() {

    //     jobsList.subscribe(() => jobs = jobsList) //<--gotta learn how to unsubscribe properly
    // }
    // //$: $jobs, flattenEmployeeNames() <--- sweet
    // //jobsList.subscribe($test => flattenEmployeeNames($test))
    // // function subscribeIgnoreFirst() {
    // //     let firedFirst = false;
    // //     return jobsList.subscribe(state => {
    // //         if (!firedFirst) {
    // //             console.log('firedfirst');
    // //             firedFirst = true;
    // //         } else {
    // //             flattenEmployeeNames(state);
    // //         }
    // //     })
    // // }
    // beforeUpdate(() => console.log('before update ran'));

    
    // afterUpdate(() => {
    // })
    // onDestroy(()=> {
    //     console.log('on destroy ran')
    //     unsub();
    // })

    // function flattenEmployeeNames() {
    //     var jobsFromStore = $jobs
    //     console.log('jobsfromstore', jobsFromStore);
        
    //     if (jobsFromStore.length === 0) {
    //         console.log('length return ran')
    //         return;
    //     }
        
    //     for(let x=0; x <= jobsFromStore.length-1; x++) {
            
    //         var employeeString = '';
    //         for (let i=0; i < jobsFromStore[x].employeeList.length; i++) {
    //             console.log('jobsFromStore[x].employeeList.length',jobsFromStore[x].employeeList.length)
    //             if (i < jobsFromStore[x].employeeList.length - 1) {
    //                 employeeString+=`${employeeString += jobsFromStore[x].employeeList[i]['firstName']} ${jobsFromStore[x].employeeList[i]['lastName']}, \n`
    //             } else {
    //                 employeeString += jobsFromStore[x].employeeList[i]['firstName'] + ' ' + jobsFromStore[x].employeeList[i]['lastName'];
    //             }    
    //         }
    //         jobsFromStore[x].employeeList = employeeString;
    //     }
    //     console.log ('jobsFromStore at end', jobsFromStore);
    //     //jobs = jobsFromStore
    //     goAhead = true;
    //     console.log('reached bottom', goAhead);
    // }

    // onMount(()=>{
    //     console.log('onmount ran')
        
    // })

$: console.log("JOBS", $jobs);
</script>

{ #if $jobs.length > 0 }

    <ContentTable
        tableContent = { jobs }
        tableSettings = { jobsTableData }
    />
{ :else }
    <h4>No Jobs Found</h4>
{ /if }

