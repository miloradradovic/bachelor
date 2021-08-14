import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {LogIn} from '../model/login.model';
import {Observable} from 'rxjs';
import {RegisterUserModel} from '../model/register-user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private headers = new HttpHeaders({'Content-Type': 'application/json'});

  constructor(private http: HttpClient) { }

  register(data: RegisterUserModel): Observable<any> {
    return this.http.post('https://localhost:5001/api/users/register',
        data, {headers: this.headers, responseType: 'json'});
  }

  verify(data: string): Observable<any> {
    return this.http.get('https://localhost:5001/api/users/verify/' + data, {headers: this.headers, responseType: 'json'})
  }
}
