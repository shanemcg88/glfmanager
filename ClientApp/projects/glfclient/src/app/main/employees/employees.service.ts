import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Employee } from './employee';
import { environment } from 'projects/glfclient/src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {
  rootUrl = '';
  constructor(private http: HttpClient) {
    this.rootUrl = environment.apiUrl + '/employee';
  }

  getEmployees() {
    return this.http.get<Employee[]>(`${this.rootUrl}`);
  }
}
