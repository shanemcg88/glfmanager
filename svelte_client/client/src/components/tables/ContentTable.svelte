<script>
    import { onMount } from "svelte";

    export let tableSettings;
    export let tableContent;

    let numberOfDataDisplayed = 1;
    let dataDisplayed = $tableContent;

    // Pagination variables. Fix showData to dataDisplayed?
    let pageCounter = 0;
    var showData = [];

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

    function pagination(num) {
        console.log('pagination ran');

        let innerIter=0
        let iterateNum = 0;
        for(let i = 0; i < $tableContent.length; i++) {
            console.log('ITERATE NUM START', iterateNum);
            /*
                we want 3 items per page 
                numberdatadisplayed * 1 = 3 -> 0, 1, 2
                    var iter = numberdatadisplay * i - 1
                        loop through tablecontent[0][1][2]
                        for(let x=0; x < iter; x++) {
                            tablecontent[x]
                        }

                numbedatadisplayed * 2 = 6 -> 3, 4, 5
                    var iter = numberdatadisplay * i - 1;
                    loop through tablecontent[3][4][5]
                    for(let x=iter-numberdatadisplayed; x < iter; x++)
                numberdatadisplayed * 3 = 9 -> 6, 7, 8
            */
            // if (numberOfDataDisplayed === 1) {
            //     iterateNum = 1;
            // }
            if ((numberOfDataDisplayed * i - 1) <= 0) {
                console.log('this if ran?');
                iterateNum = numberOfDataDisplayed;
            }
            else {
                let calc = numberOfDataDisplayed * i - 1;
                console.log(`iterateNum ${iterateNum} += ${calc} `)
                iterateNum += calc;
            }

            console.log('iterateNum BEFORE', iterateNum);
            for(let x = iterateNum - numberOfDataDisplayed; x <= iterateNum-1; x++) {
                if (iterateNum === 1 && i > 0) {
                    x = 1;
                }
                console.log('X=', x);
                console.log("CONTENT", $tableContent[x]);
            }
            console.log('ITERATENUM AFTER', iterateNum);
            
        }


        console.log('pageCounter=', pageCounter);
    }

    onMount(() => {
        setDisplayNumber(numberOfDataDisplayed)
        pagination();
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

{#each showData as data}

{/each}
