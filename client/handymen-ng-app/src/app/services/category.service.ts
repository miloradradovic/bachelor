import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private headers = new HttpHeaders({'Content-Type': 'application/json'});

  constructor(private http: HttpClient) { }

  getCategories(): Observable<any> {
    return this.http.get('https://localhost:5001/api/categories', {headers: this.headers, responseType: 'json'});
  }

  getCategoriesWithProfessions(): Observable<any> {
    return this.http.get('https://localhost:5001/api/categories/get-categories-with-professions', {headers: this.headers, responseType: 'json'});
  }
}
