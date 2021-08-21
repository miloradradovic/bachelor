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
}
