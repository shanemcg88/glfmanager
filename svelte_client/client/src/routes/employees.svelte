<script>
    import { goto } from '$app/navigation';
    import { getEmployees } from '../shared/fetch.svelte';

    import { ROOT_URL } from '../stores';

    let employees;
    let rootUrl;
    //let bearer = localStorage.getItem('accessToken');

    ROOT_URL.subscribe(value => rootUrl = value);

    getEmployees()
        .then((emp) => employees = emp)
        .catch((err) => console.log('ERRORHAPPENED IN EMPLOYEES', err));

</script>

<h1>Employees</h1>

{#await getEmployees()}
    <p>Getting employees...</p>
{:then}
    <h1>Employees loaded</h1>
{/await}