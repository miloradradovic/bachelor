import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  private headers = new HttpHeaders({'Content-Type': 'application/json'});

  constructor(private http: HttpClient) { }

  getLocation() : Observable<any> {
    return this.http.get('https://localhost:5001/api/locations/get-location', {headers: this.headers, responseType: 'json'});
  }
}
