<script>
    import { getDailyJobs } from '../shared/globalFetch.svelte';
    import JobsComponent from '../components/Jobs/JobsComponent.svelte';
import { onMount } from 'svelte';

    var today = new Date().toISOString();
    console.log('today', today);
    console.log('Date', Date);
    console.log('Date.UTC', Date.UTC);
</script>

<h1>Jobs</h1>

{#await getDailyJobs(today)}
    <p>Getting clients...</p>
    {:then}
        <JobsComponent />

    {:catch error}
        <h4>{ error }</h4>
{/await}