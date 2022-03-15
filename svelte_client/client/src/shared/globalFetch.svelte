<script context="module">
    import { employeeList, clientList } from '../stores';
    import { signedIn } from '../auth';
    import { goto } from '$app/navigation';
    import { browser } from '$app/env';

    const rootUrl = import.meta.env.VITE_ROOTURL;
    let bearer;
    let responseError = '';

    // if (browser && localStorage.getItem("accessToken")) {
    //     bearer = localStorage.getItem("accessToken")
    // }
    
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
        });
        if (response.ok) {
            localStorage.clear();
            signedIn.update(value => value = false);
            goto('/login');
        } else {
            throw new Error("Something went wrong logging out");
        }
    }


    export async function userAuth() {
        await fetch(`${rootUrl}/useraccount/auth`, {
            method: 'GET',
            mode: 'cors', 
            credentials: 'include'
        }).then(res => {
            // if idserver cookie expires
            if (res.status===401){
                signedIn.update(value => value = false);
                goto('/login')
            }
            if (res.ok)
                // signedIn will lose it's state if page is refreshed
                signedIn.update(value => value = true);
            return res.json();

        }).then((res)=>{
            if (res.IsAuth)
                signedIn.update((value) => value = true)
        }).catch(() => {
            // Redirect user to login page if any errors occur
            signedIn.update(value => value = false);
            goto('/login')     
        });
    }

    export async function getEmployees() {      
        let bearer = localStorage.getItem("accessToken");
        await fetch(`${rootUrl}/employee`, {
            method: 'GET',
            mode: 'cors', 
            credentials: 'include',
            headers: {
                'Authorization': `Bearer ${bearer}`,
                'Content-Type': 'application/json'
            }
        }).then(res => {
            res.ok ? res.json()
            .then(data => employeeList.update(value => value = data))
            : signOut();
        }).catch(() => { throw new Error("Something went wrong grabbing employees") })
    }

    export async function getClients() {
        let bearer = localStorage.getItem("accessToken");
        await fetch(`${rootUrl}/company`, {
            method: 'GET',
            mode: 'cors', 
            credentials: 'include',
            headers: {
                'Authorization': `Bearer ${bearer}`,
                'Content-Type': 'application/json'
            }
        }).then(res => {
            res.ok? res.json()
            .then(data => clientList.update(value => value = data))
            : signOut();
        }).catch(() => { throw new Error("Something went wrong when grabbing clients") })
    }
</script>