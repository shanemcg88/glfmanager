import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Login } from './login';

interface LoginResponse {
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
export class LoginService {
  loginUrl = 'http://localhost:33000/api/useraccount/login';
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };
  
  constructor(private http: HttpClient) { }

  login(credentials: Login) {
    credentials.clientId="mobile";
    return this.http.post<LoginResponse>(
      `http://localhost:33000/api/useraccount/login`, credentials, {withCredentials: true})
  }
}
