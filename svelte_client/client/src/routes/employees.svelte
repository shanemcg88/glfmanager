<script>
    import { ROOT_URL } from '../stores'

    let employees;
    let rootUrl;
    ROOT_URL.subscribe(value => rootUrl = value);

    $: console.log('employees=', employees);

    async function getEmployees() {
        console.log('getemployees ran');
        await fetch(`${rootUrl}/employee`, {
            method: 'GET',
            mode: 'cors', 
            credentials: 'include',
            
        }).then(res => {
            console.log('res', res);
            if (res.ok)
                return res.json();

        }).then((emp) => employees = emp)

          .catch((err) => console.log('ERRORHAPPENED IN EMPLOYEES', err));
    }

</script>

<h1>Employees</h1>

{#await getEmployees()}
    <p>Getting employees...</p>
{:then}
    <h1>Employees loaded</h1>
{/await}