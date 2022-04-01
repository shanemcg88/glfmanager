<script>
    import { getDailyJobs } from '../shared/globalFetch.svelte';
    import JobsComponent from '../components/Jobs/JobsComponent.svelte';

    var today = new Date().toISOString();
    //var today = new Date("2022-04-30").toISOString
</script>

<h1>Jobs</h1>

{#await getDailyJobs(today)}
    <p>Getting clients...</p>
    {:then}
        <JobsComponent />

    {:catch error}
        <h4>{ error }</h4>
{/await}