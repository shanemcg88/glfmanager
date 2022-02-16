<script>
    import { createEventDispatcher } from 'svelte';
    import { navOptions } from '../navbar/Nav.svelte'

    export let intSelected;
    
    let dispatch = createEventDispatcher();
    
    function changePage(pageNumber) {
        dispatch('changePage', {
            number: pageNumber
        });
    }
</script>

<div class="d-flex flex-column flex-shrink-0 p-3 bg-light" id="sideNav">
    <ul class="nav nav-pills nav-fill flex-column mb-auto">
		{#each navOptions as option, i}
		<li class="nav-item">
			<button 
                id="sideNavBtn"
                class={intSelected==i ? "nav-link active" : "nav-link"} 
                on:click={()=>changePage(i)}  
                role="tab">
                    {option.page}
            </button>
		</li>
		{/each}
    </ul>
</div>

<style>
    #sideNav {
        width:280px;
        height: 90vh;
    }

    #sideNavBtn {
        text-align: left;
    }
 
</style>