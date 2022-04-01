<script>
    import ContentTable from '../tables/ContentTable.svelte';
    import AddEmployeeModal from './AddEmployeeModal.svelte';
    import { employeeList } from '../../stores';
    import { employeeTableData } from './employeeTableData';

    let employees;
    employeeList.subscribe(() => employees = employeeList);
    
</script>

<nav class="navbar navbar-light bg-light employeeNav">
    <button type="button" class="btn btn-outline-primary addEmployeeBtn">+ Add Employee</button>

</nav>

{ #if $employees.length > 0 }
    <ContentTable 
        tableContent = { employees }
        tableSettings = { employeeTableData }
    />
{ :else }
    <h4>No employees found</h4>
{ /if }

<style>
    .employeeNav {
        margin-bottom: 10px;
        padding: 5px;
    }

    h4 {
        position: absolute;
        left: 50%;
        top: 40%;
        transform: translateY(-60%);
    }
</style>