<script context="module">
    import { signedIn } from '../auth';
    import { goto } from '$app/navigation';
    import { browser } from '$app/env';

    const rootUrl = import.meta.env.VITE_ROOTURL;
    let bearer;
    let responseError = '';

    if (browser && localStorage.getItem("accessToken")) {
        bearer = localStorage.getItem("accessToken")
    }
    
    export async function loginFetch(userName, password) {
        return await fetch(`${rootUrl}/useraccount/login`, {
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
    }

    export async function signOut() {
        const response = await fetch(`${rootUrl}/useraccount/logout`, {
            method: 'GET',
            mode: 'cors',
            credentials: 'include'
        })
        if (response.ok) {
            localStorage.clear();
            signedIn.update(value => value = false);
            goto('/login');
        } else {
            throw new Error("Something went wrong logging out");
        }
    }

    export async function getEmployees() {      
        console.log('bearer', bearer);  
        await fetch(`${rootUrl}/employee`, {
            method: 'GET',
            mode: 'cors', 
            credentials: 'include',
            headers: {'Authorization': `Bearer ${bearer}`}
        }).then(res => {
            res.ok ? res.json() : signOut();
        })   
    }
</script>