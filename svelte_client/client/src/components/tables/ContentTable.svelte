<script>
    import { onMount } from "svelte";

    export let tableSettings;
    export let tableContent;

    let numberOfDataDisplayed = 5;
    let dataDisplayed = $tableContent;

    function setDisplayNumber(displayNum) {

        dataDisplayed = [];

        if ($tableContent.length < displayNum) {
            displayNum = $tableContent.length;
        }

        for (let i = 0; i < displayNum; i++) {
            dataDisplayed.push($tableContent[i]);
        }

        dataDisplayed = dataDisplayed;
    }

    function sortColumn(index, column, isSorted) {

        let columnData = [];
        dataDisplayed.forEach((x) =>{
            columnData.push(x[column]);
        })

        if (!isSorted) {
            columnData.sort();
            tableSettings[index]['isSorted'] = true;
        } else {
            columnData.sort();
            columnData.reverse();
            tableSettings[index]['isSorted'] = false;
        }

        let newSortedData = [];

        columnData.forEach(x => {
            var match = dataDisplayed.find(data => data[column] === x);
            newSortedData.push(match);
        });

        dataDisplayed = dataDisplayed = newSortedData;

    }

    onMount(() => setDisplayNumber(numberOfDataDisplayed))

</script>

<table class="table table-striped table-hover">
    <thead>
        <tr>
            {#each tableSettings as setting, i}
                <th>
                    {setting.heading}
                    {#if setting.sort}
                        <button 
                            on:click={()=>sortColumn(i, setting.dataKey, setting.isSorted)}
                        >
                            x
                        </button>
                    {/if}
                </th>
            {/each}
        </tr>
    </thead>
    <tbody>
        {#each dataDisplayed as data}
            <tr>
                {#each tableSettings as setting}
                    <td>{data[setting.dataKey]}</td>
                {/each}
            </tr>
        {/each}
    </tbody>

</table>
<select bind:value={numberOfDataDisplayed} on:change={()=>setDisplayNumber(numberOfDataDisplayed)}>
    <option value = 1> 1 </option>
    <option value = 2> 2 </option>
    <option value = 3> 3 </option>
</select>