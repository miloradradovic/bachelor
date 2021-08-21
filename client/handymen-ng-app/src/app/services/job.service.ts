import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JobService {

  private headers = new HttpHeaders({'Content-Type': 'application/json'});

  constructor(private http: HttpClient) { }

  createJob(id: number): Observable<any> {
    return this.http.post('https://localhost:5001/api/jobs/create-job/' + id, null, {headers: this.headers, responseType: 'json'});
  }

  getJobsForUser(): Observable<any> {
    return this.http.get('https://localhost:5001/api/jobs/get-jobs-by-user', {headers: this.headers, responseType: 'json'});
  }

  getJobsForHandyman(): Observable<any> {
    return this.http.get('https://localhost:5001/api/jobs/get-jobs-by-handyman', {headers: this.headers, responseType: 'json'});
  }

  finishJob(id: number): Observable<any> {
    return this.http.put('https://localhost:5001/api/jobs/finish-job/' + id, null, {headers: this.headers, responseType: 'json'});
  }
}
