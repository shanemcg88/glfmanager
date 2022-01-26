import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Client } from './client'
// import { environment } from 'projects/glfclient/src/environments/environment';
import { environment } from '../../../environments/environment';
import { CoreEnvironment } from '@angular/compiler/src/compiler_facade_interface';

@Injectable({
  providedIn: 'root'
})
export class ClientsService {  
  rootUrl = '';
  constructor(private http: HttpClient) {
    this.rootUrl = environment.apiUrl  + '/company';
   }

  getClients() {
    return this.http.get<Client[]>(`${this.rootUrl}`);
  }
}
