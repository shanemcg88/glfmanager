<script>
    import { signedIn } from '../auth';
    import { goto } from '$app/navigation';
    import { loginFetch } from '../shared/globalFetch.svelte';

    let userName = 'shanelgmcguire@gmail.com';
    let password = 'Password1';
    let disabled = true;
    let loginError = '';
    let isError = false;    

    $: if (loginError.length > 0)
        isError = true;

    $: if (userName.length > 0 && password.length > 0)
            disabled = false;
        else 
            disabled = true;

    async function loginSubmit() {
        // disabling login button
        disabled = true;
        
        await loginFetch(userName, password)
            .then(response => {
                if (response.ok) {
                    return response.json();
                } else {
                    response.status === 401 ? 
                        loginError = 'Invalid login credentials' :
                        loginError = 'Something went wrong. Please try again or contact support';   
                }

                // resetting login button
                disabled=false;

            }).then(res => {
                window.localStorage.setItem('accessToken', res.accessToken);
                signedIn.update(x => x = true);
                goto(`/jobs`, {replaceState:true});
            }).catch(() =>loginError = 'Something went wrong. Please try again or contact support')
    }

    // disabling user from pressing back on the login page and returning to the main page
    function beforeunload(event) {
        event.preventDefault();
        return event.returnValue = '';
    }

</script>
<svelte:window on:beforeunload={beforeunload}/>

<div class="text-center">
    <div class="loginContainer">
        <form 
            class="loginForm"
            on:submit|preventDefault={loginSubmit}
        >
            <h1 class="h3 mb-3 fw-normal">
                GLF Manager
            </h1>
            
            <div class="form-floating">
                <input
                    class="form-control"
                    id="floatingInput"
                    type="text" 
                    bind:value={userName}
                    on:input={() => userName}
                />
                <label for="floatingInput">Email Address</label>
            </div>

            <div class="form-floating mt-2">
                <input
                    class="form-control"
                    id="floatingPassword"
                    type="password"
                    bind:value={password}
                    on:input={() => password}
                />
                <label for="floatingPassword">Password</label>
            </div>
            
            <button
                class="w-100 btn btn-lg btn-primary mt-2"
                type="submit"
                {disabled}
            >
                Login
            </button>
            {#if isError}
                <p>{loginError}</p>
            {/if}
        </form>
    </div>
</div>

<style>
    .loginContainer {
        display: flex;
        flex-wrap: wrap;
        justify-content: center;
        align-items: center;
        height: 95vh;
        align-content: space-around;
        -webkit-align-content: space-around; 
    }

    .loginForm {
        min-width: 200px;
        width: 50%;
        max-width: 500px;
    }
</style>
