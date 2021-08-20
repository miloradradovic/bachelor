import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {InterestModel} from '../model/interest.model';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InterestService {

  private headers = new HttpHeaders({'Content-Type': 'application/json'});

  constructor(private http: HttpClient) { }


  createInterest(interest: InterestModel): Observable<any> {
    return this.http.post('https://localhost:5001/api/interests/create-interest', interest, {headers: this.headers, responseType: 'json'});
  }
}
