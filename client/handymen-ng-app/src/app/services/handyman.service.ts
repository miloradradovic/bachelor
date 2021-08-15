import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {RegisterUserModel} from '../model/register-user.model';
import {Observable} from 'rxjs';
import {RegisterDataModel} from '../model/register-data.model';

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
}
