<script>
import { onMount } from "svelte";

    export let tableSettings;
    export let tableContent;

    let numberOfDataDisplayed = 3;
    $: dataDisplayed = [];
    let data = $tableContent;
    // Pagination variables. Fix showData to dataDisplayed?
    let pageCounter = 0;
    var showData = [];
    let pages = 0;

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
        data.forEach((x) =>{
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
            var match = data.find(data => data[column] === x);
            newSortedData.push(match);
        });

        data = data = newSortedData;
        pagination(1);
    }

    function pagination(pageSelected) {

        let end = pageSelected * numberOfDataDisplayed - 1;
        let start = end - numberOfDataDisplayed + 1

        let currentDisplay=[];

        for (let x = start; x <= end; x++) {

            if (data[x])
                currentDisplay.push(data[x]);
            else
                break;

        }

        dataDisplayed = currentDisplay;
    }

    function pageCalculator() {

        let totalData = $tableContent.length;
        let calc = Math.floor(totalData / numberOfDataDisplayed);

        if (totalData % numberOfDataDisplayed > 0) {
            calc = calc + 1;
        }

        return pages = calc;
    }

    onMount(() => {
        setDisplayNumber(numberOfDataDisplayed);
        pageCalculator();
        pagination(1);
    })

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

{#each Array(pages) as _, i}
    <li on:click={()=>pagination(i+1)}>{i+1}</li>
{/each}
