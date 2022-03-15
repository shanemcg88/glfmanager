<script>
    import { getEmployees } from '../shared/globalFetch.svelte';
    import EmployeesComponent from '../components/Employees/EmployeesComponent.svelte';

</script>

<h1>Employees</h1>

{#await getEmployees()}
    <p>Getting employees...</p>
{:then}
    <EmployeesComponent/>
{:catch error}
    <h1>{ error }</h1>
{/await}