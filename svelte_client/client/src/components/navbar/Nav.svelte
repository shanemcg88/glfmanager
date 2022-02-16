<script context="module">
    import Clients from "../../routes/clients.svelte";
    import Employees from "../../routes/employees.svelte";
    import Jobs from "../../routes/jobs.svelte";
    
    export const navOptions = [
        { page: 'Jobs', component: Jobs },
        { page: 'Employees', component: Employees },
        { page: 'Clients', component: Clients}
    ]
</script>