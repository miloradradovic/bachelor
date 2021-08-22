import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OfferService {

  private headers = new HttpHeaders({'Content-Type': 'application/json'});

  constructor(private http: HttpClient) { }

  makeOffer(param: { handyMan: number; jobAd: any }): Observable<any> {
    return this.http.post('https://localhost:5001/api/offers/make-offer', param, {headers: this.headers, responseType: 'json'});
  }

  getOffersByHandyman(): Observable<any> {
    return this.http.get('https://localhost:5001/api/offers/get-offers-by-handyman', {headers: this.headers, responseType: 'json'});
  }
}
