import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {RegisterDataModel} from '../model/register-data.model';
import {SearchParams} from '../model/search-params';
import {ProfileDataModel} from '../model/profile-data.model';
import {HandymanVerificationModel} from '../model/handyman-verification.model';

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

  getCurrentHandyman(): Observable<any> {
    return this.http.get('https://localhost:5001/api/handymen/get-current-handyman', {headers: this.headers, responseType: 'json'});
  }

  editProfile(profileData: ProfileDataModel): Observable<any> {
    return this.http.put('https://localhost:5001/api/handymen/edit-profile', profileData, {headers: this.headers, responseType: 'json'});
  }

  getRequests(): Observable<any> {
    return this.http.get('https://localhost:5001/api/handymen/get-requests', {headers: this.headers, responseType: 'json'});
  }

  verify(handymanVerificationModel: HandymanVerificationModel): Observable<any> {
    return this.http.put('https://localhost:5001/api/handymen/verify', handymanVerificationModel, {headers: this.headers, responseType: 'json'});
  }

  getHandymenByProfession(profession): Observable<any> {
    return this.http.get('https://localhost:5001/api/handymen/get-by-profession/' + profession,
      {headers: this.headers, responseType: 'json'});
  }

  filter(searchParams: SearchParams): Observable<any> {
    return this.http.post('https://localhost:5001/api/handymen/filter', searchParams, {headers: this.headers, responseType: 'json'});
  }
}
