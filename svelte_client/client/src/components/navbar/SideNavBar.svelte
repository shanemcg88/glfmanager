<script>
    import { createEventDispatcher } from 'svelte';
    import { navOptions } from '../navbar/Nav.svelte'
    import { page } from '$app/stores'
    
    let currentPage
    page.subscribe(value => currentPage = value.url.pathname);
    console.log('currentPage', currentPage);

</script>

<div class="d-flex flex-column flex-shrink-0 p-3 bg-light" id="sideNav">
    <ul class="nav nav-pills nav-fill flex-column mb-auto">
		{#each navOptions as option, i}
            <li class="nav-item">
                <a 
                    href={`/${option.page.toLowerCase()}`}
                    class={currentPage===`/${option.page.toLowerCase()}` ? "nav-link active" : "nav-link"} 
                >
                    {option.page}
                </a>
                <!-- <button 
                    id="sideNavBtn"
                    class={intSelected==i ? "nav-link active" : "nav-link"} 
                    on:click={()=>changePage(i)}  
                    role="tab">
                        {option.page}
                </button> -->
            </li>
		{/each}
    </ul>
</div>

<style>
    #sideNav {
        width:280px;
        height: 90vh;
    }

    .nav-link {
        text-align: left;
    }
 
</style>