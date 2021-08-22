import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {RegisterDataModel} from '../model/register-data.model';
import {SearchParams} from '../model/search-params';

@Injectable({
  providedIn: 'root'
})
export class HandymanService {

  private headers = new HttpHeaders({'Content-Type': 'application/json'});

  constructor(private http: HttpClient) { }

  register(data: RegisterDataModel): Observable<any> {
    return this.http.post('https://localhost:5001/api/handymen/register',
      data, {headers: this.headers, responseType: 'json'});
  }

  getAllHandymen(): Observable<any> {
    return this.http.get('https://localhost:5001/api/handymen', {headers: this.headers, responseType: 'json'});
  }

  search(searchParams: SearchParams): Observable<any> {
    return this.http.post('https://localhost:5001/api/handymen/search', searchParams, {headers: this.headers, responseType: 'json'});
  }

  getHandymanById(data: number): Observable<any> {
    return this.http.get('https://localhost:5001/api/handymen/get-handyman-by-id/' + data, {headers: this.headers, responseType: 'json'});
  }
}
