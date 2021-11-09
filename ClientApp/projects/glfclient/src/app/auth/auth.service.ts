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
    console.log('cookie=',this.cookieService.getAll());
  }

  signIn(credentials: SignInCredentials) {
    credentials.clientId="mobile";
    
    return this.http.post<SignInResponse> (`
      ${this.rootUrl}/login`, 
      credentials, 
      { withCredentials: true }
    ).pipe(
      tap(() => {
        this.signedIn$.next(true);
      })
    )
  }
}
