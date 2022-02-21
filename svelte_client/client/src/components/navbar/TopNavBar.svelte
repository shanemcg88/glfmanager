<script>
  import { goto } from "$app/navigation";
  import { signOut } from '../../shared/globalFetch.svelte'

</script>

<ul class="col nav">
    <li class="nav-item">
      <h3 class="navbar-brand">GLF Manager</h3>
    </li>
    <li class="nav-item" id="logoutBtn" on:click={()=>signOut()}>
      <span class="nav-link" tabindex="-1" aria-disabled="true">Logout</span>
    </li>
</ul>

<style>
  ul {
    justify-content: space-between;
    margin-left: auto;
    margin-right: auto;
    width: 95vw;
  }
  
  #logoutBtn:hover{
    cursor:pointer;
  }
  
</style>