import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {RatingModel} from '../model/rating.model';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RatingService {

  private headers = new HttpHeaders({'Content-Type': 'application/json'});

  constructor(private http: HttpClient) { }

  createRating(rating: RatingModel): Observable<any> {
    return this.http.post('https://localhost:5001/api/ratings/create-rating', rating, {headers: this.headers, responseType: 'json'});
  }
}
