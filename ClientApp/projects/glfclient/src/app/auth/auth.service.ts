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

interface CheckAuthResponse {
  isAuth: boolean;
  message: string;
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
    var bearer = 'Bearer '+this.cookieService.get('Bearer');
    const httpOptions = new HttpHeaders({
      'Content-Type':  'application/json',
      Authorization: bearer,
    })
    return this.http.get<CheckAuthResponse>(`${this.rootUrl}/auth`, { headers: httpOptions, withCredentials: true })
      .pipe(
        tap(({ isAuth }) => {
          this.signedIn$.next(isAuth);
        })
      );
  }

  signIn(credentials: SignInCredentials) {
    credentials.clientId="mobile";
    return this.http.post<SignInResponse> (`${this.rootUrl}/login`, credentials, {withCredentials: true})
    .pipe(
      tap((response) => {
        if (response.accessToken) {
          document.cookie=`Bearer=${response.accessToken};max-age=${response.expires};samesite=lax;httponly` + location.hostname;
        }
        this.signedIn$.next(true);
      })
    )
  }
}
