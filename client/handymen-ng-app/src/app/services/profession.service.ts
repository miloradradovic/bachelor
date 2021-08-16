import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProfessionService {

  private headers = new HttpHeaders({'Content-Type': 'application/json'});

  constructor(private http: HttpClient) { }

  getProfessionsByCategory(categoryId: number): Observable<any> {
    return this.http.get('https://localhost:5001/api/professions/get-professions-by-category/' + categoryId, {headers: this.headers, responseType: 'json'})
  }
}
