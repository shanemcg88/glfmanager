<script>
import { onMount } from "svelte";
import IoIosArrowDown from 'svelte-icons/io/IoIosArrowDown.svelte';
import IoIosArrowUp from 'svelte-icons/io/IoIosArrowUp.svelte';

    export let tableSettings;
    export let tableContent;

    let numberOfDataDisplayed = 3;
    $: dataDisplayed = [];
    let data = $tableContent;
    let pages = 0;
    let pageSelected = 1;

    function setDisplayNumber(displayNum) {

        dataDisplayed = [];

        if ($tableContent.length < displayNum) {
            displayNum = $tableContent.length;
        }

        for (let i = 0; i < displayNum; i++) {
            dataDisplayed.push($tableContent[i]);
        }

        dataDisplayed = dataDisplayed;
        pageCalculator();

    }

    function sortColumn(index, column, isSorted) {
        console.log('isSorted = ', isSorted);
        
        let columnData = [];
        data.forEach((x) =>{
            columnData.push(x[column]);
        });

        if (isSorted === false) {
            
            // Reset table data to how it loaded originally
            columnData = []
            $tableContent.forEach((x) =>{
                columnData.push(x[column]);
            })
            tableSettings[index]['isSorted'] = null;

        } else if (isSorted === true) {

            // Descending
            columnData.sort();
            columnData.reverse();
            tableSettings[index]['isSorted'] = false;

        } else if (isSorted === null) {
            
            // Ascending
            columnData.sort();
            tableSettings[index]['isSorted'] = true

        }

        let newSortedData = [];

        columnData.forEach(x => {
            var match = data.find(data => data[column] === x);
            newSortedData.push(match);
        });

        data = data = newSortedData;
        pagination(pageSelected);
    }

    function pagination(page) {

        // max previous and next cases
        if (page === 0 || page === pages + 1)
            return;

        let end = page * numberOfDataDisplayed - 1;
        let start = end - numberOfDataDisplayed + 1

        let currentDisplay=[];

        for (let x = start; x <= end; x++) {

            if (data[x])
                currentDisplay.push(data[x]);
            else
                break;

        }

        pageSelected = page;
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
    <!-- Main Table Column Headings -->
    <thead>
        <tr>
            { #each tableSettings as setting, i }
                <th on:click={()=>sortColumn(i, setting.dataKey, setting.isSorted)}>
                    { setting.heading }
                    { #if setting.sort }
                        <button
                            class="icon"
                        >
                            { #if (setting.isSorted) }
                                <IoIosArrowDown />
                            { :else if (setting.isSorted === false) }
                                <IoIosArrowUp />
                            { /if }

                        </button>
                    { /if }
                </th>
            { /each} 
        </tr>
    </thead>

    <!-- Main Table Data -->
    <tbody>
        { #each dataDisplayed as data }
            <tr>
                { #each tableSettings as setting }
                    <td>{ data[setting.dataKey] }</td>
                { /each }
            </tr>
        { /each }
    </tbody>

</table>


<!-- Pagination  -->
<div class="pageSelection d-flex justify-content-between">

    <!-- Number of data to display -->
    <div class="displayDataNumber">
        <select
            class = "form-select form-select-md"
            bind:value={numberOfDataDisplayed} 
            on:change={()=>setDisplayNumber(numberOfDataDisplayed)}
        >
            <option value = { numberOfDataDisplayed }> Display </option>
            <option value = 1> 1 </option>
            <option value = 2> 2 </option>
            <option value = 3> 3 </option>
        </select>
    </div>

    <ul class="pagination">

        <!-- Previous Button -->
        <li 
            class = {
                pageSelected === 1 ? 'page-item disabled' : 'page-item'
            }
            on:click={ ()=>pagination(pageSelected - 1) }
        >
            <span class = "page-link">
                Previous
            </span>
        </li>

        <!-- Page Numbers -->
        { #each Array(pages) as _, i }
            <li 
                class = {
                    pageSelected === i+1 ? 'page-item active' : 'page-item'
                }
                on:click={ ()=>pagination(i+1) }
            >
                <span class="page-link">{ i+1 }</span>
            </li>
        { /each }

        <!-- Next Button -->
        <li 
            class = {
                pageSelected === pages ? 'page-item disabled' : 'page-item'
            }
            on:click={ ()=>pagination(pageSelected + 1) }
        >
            <span class = "page-link">
                Next
            </span>
        </li>
    </ul>
</div>

<style>
    .icon {
        color: black;
        background: none;
        border: none;
        width: 32px;
        height: 32px;
    } 
</style>
