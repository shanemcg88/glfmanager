<script>
    import ContentTable from '../tables/ContentTable.svelte';
    import { employeeList } from '../../stores';
    import { employeeTableData } from './employeeTableData';

    let employees;
    employeeList.subscribe(() => employees = employeeList);
    
</script>

{ #if $employees.length > 0 }
    <ContentTable 
        tableContent = { employees }
        tableSettings = { employeeTableData }
    />
{ /if }