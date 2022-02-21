<script>
    import ContentTable from '../tables/ContentTable.svelte';
    import { employeeList } from '../../stores';

    let employees;
    employeeList.subscribe(() => employees = employeeList);
    
    $: console.log('$employees.length', $employees);
</script>

<h1>EmployeesComponent</h1>
{#if $employees.length > 0}
    <ContentTable/>
{/if}