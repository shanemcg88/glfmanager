import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

interface AllCompanies {
  clients: [];
}

@Injectable({
  providedIn: 'root'
})
export class ClientsService {
  rootUrl = 'http://localhost:33000/api/company';
  constructor(private http: HttpClient) { }

  getClients() {
    return this.http.get<AllCompanies>(`${this.rootUrl}`);
  }
}
