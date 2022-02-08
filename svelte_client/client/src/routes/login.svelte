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
        try {
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
            })
            if (response.ok) {
                signedIn.update(x => x = true);
                goto(`/`, {replaceState:true})
            } else {
                if (response.status === 401)
                    loginError = 'Invalid login credentials';
                else 
                    loginError = 'Something went wrong. Please try again or contact support';
            }
        } catch (err) {
            loginError = 'Something went wrong. Please try again or contact support';
        }
    }
</script>

<h1>Login</h1>
<form on:submit|preventDefault={loginSubmit}>
    <input 
        type="text" 
        bind:value={userName}
        on:input={() => userName}
    />
    <input
        type="password"
        bind:value={password}
        on:input={() => password}
    />

    <button 
        type="submit"
        {disabled}
    >
        Login
    </button>
    {#if isError}
        <p>{loginError}</p>
    {/if}
</form>
