import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TradeService {

  private headers = new HttpHeaders({'Content-Type': 'application/json'});

  constructor(private http: HttpClient) { }

  getTrades(): Observable<any> {
    return this.http.get('https://localhost:5001/api/trades/get-trades', {headers: this.headers, responseType: 'json'})
  }

  getTradesByProfession(professionId: number): Observable<any> {
    return this.http.get('https://localhost:5001/api/trades/get-trades/' + professionId, {headers: this.headers, responseType: 'json'})
  }

  getCurrentHandymanCategoryAndProfession(): Observable<any> {
    return this.http.get('https://localhost:5001/api/trades/get-profession-and-category-by-trades', {headers: this.headers, responseType: 'json'});
  }

  getTradesByProfessionName(profession: string): Observable<any> {
    return this.http.get('https://localhost:5001/api/trades/get-trades-by-profname/' + profession, {headers: this.headers, responseType: 'json'});

  }
}
