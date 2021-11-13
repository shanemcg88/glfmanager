import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';

interface SignInCredentials {
  email: string;
  password: string;
  clientId: string;
}

interface SignInResponse {
  accessToken: string;
  expires: number;
  user: {
    id: string;
    userName: string;
    email: string;
  },
  message: string | null
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  rootUrl = 'http://localhost:33000/api/useraccount';
  signedIn$ = new BehaviorSubject<boolean | null>(null);
  bearer: string = '';
  
  constructor(
    private http: HttpClient,
  ) { }


  checkAuth() {
    const httpOptions = new HttpHeaders({
      'Content-Type':  'application/json',
      Authorization: this.bearer,
    })
    return this.http.get(`${this.rootUrl}/auth`, {headers: httpOptions, withCredentials: true});
  }

  signIn(credentials: SignInCredentials) {
    credentials.clientId="mobile";
    return this.http.post<SignInResponse> (`${this.rootUrl}/login`, credentials, {withCredentials: true})
    .pipe(
      tap((response) => {
        console.log('login response: ', response);
        if (response.accessToken) {
          console.log('localstorage if ran', response);
          this.bearer="Bearer "+response.accessToken;
          // console.log()
        }
        this.signedIn$.next(true);
      })
    )
  }
}
