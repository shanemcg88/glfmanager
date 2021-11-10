import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { CookieService } from 'ngx-cookie-service';

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

  constructor(
    private http: HttpClient,
    private cookieService: CookieService
  ) { }

  checkAuth() {
    var cookies=document.cookie;
    console.log('cookies', cookies);
  }

  signIn(credentials: SignInCredentials) {
    credentials.clientId="mobile";
    return this.http.post<SignInResponse> (`${this.rootUrl}/login`, credentials)
    .pipe(
      tap((response) => {
        console.log('login response: ', response);
        if (response.accessToken) {
          console.log('localstorage if ran');
          //document.cookie = `${response.accessToken}; SameSite=none; Secure`
          document.cookie = '=; Max-Age=0;' + location.hostname;
        }
        this.signedIn$.next(true);
      })
    )
  }
}
