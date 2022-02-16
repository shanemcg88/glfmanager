<script>
  import { goto } from "$app/navigation";
  import { signedIn } from "../../auth.js";

  let rootUrl = import.meta.env.VITE_ROOTURL;

  const signOut = async () => {
      const response = await fetch(`${rootUrl}/useraccount/logout`, {
          method: 'GET',
          mode: 'cors',
          credentials: 'include'
      })

      if (response.ok) {
          signedIn.update(value => value = false);
          goto('/login');
      } else {
          throw new Error("Something went wrong logging out");
      }
  }
</script>

<ul class="col nav">
    <li class="nav-item">
      <h3 class="navbar-brand">GLF Manager</h3>
    </li>
    <!-- <li class="nav-item">
      <a class="nav-link active" aria-current="page" href="#">Active</a>
    </li>
    <li class="nav-item">
      <a class="nav-link" href="#">Link</a>
    </li>
    <li class="nav-item">
      <a class="nav-link" href="#">Link</a>
    </li> -->
    <li class="nav-item" id="logoutBtn" on:click={signOut}>
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