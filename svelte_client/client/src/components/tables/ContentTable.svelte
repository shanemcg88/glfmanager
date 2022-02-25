<script>
    export let tableSettings;
    export let tableContent;

    function sortColumn(index, column, isSorted) {

        let columnData = [];
        $tableContent.forEach((x) =>{
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
            var match = $tableContent.find(data => data[column] === x);
            newSortedData.push(match);
        });

        $tableContent = $tableContent = newSortedData;
    }

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
        {#each $tableContent as data}
            <tr>
                {#each tableSettings as setting}
                    <td>{data[setting.dataKey]}</td>
                {/each}
            </tr>
        {/each}
    </tbody>

</table>