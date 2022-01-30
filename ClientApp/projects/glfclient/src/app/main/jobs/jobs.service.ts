import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Job } from './job';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class JobsService {
  rootUrl = '';
  constructor(private http: HttpClient) {
    this.rootUrl = environment.apiUrl + '/job/dailyjobs'
  }

  getDailyJobs() {
    return this.http.get<Job[]>(`${this.rootUrl}`);
  }
}
