<script>
    import { signedIn } from '../auth';
    import { goto } from '$app/navigation';

    let rootUrl = import.meta.env.VITE_ROOTURL;
    let userName = 'shanelgmcguire@gmail.com';
    let password = 'Password1';
    let disabled = true;
    let loginError = '';
    let isError = false;

    $: if (loginError.length > 0) {
        isError = true;
    }

    $: if (userName.length > 0 && password.length > 0)
            disabled = false;
        else 
            disabled = true;

    async function loginSubmit() {
        disabled = true;
            const response = await fetch(`${rootUrl}/useraccount/login`, {
                method: 'POST',
                mode: 'cors', 
                credentials: 'include',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    email: userName,
                    password,
                    clientId: "mobile"
                })
            }).then(response => {
                if (response.ok) {
                    signedIn.update(x => x = true);
                    goto(`/`, {replaceState:true})
                    return response.json();
                } else {
                    if (response.status === 401)
                        loginError = 'Invalid login credentials';
                    else 
                        loginError = 'Something went wrong. Please try again or contact support';
                    
                    // resetting login button
                    disabled=false;
                }
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
