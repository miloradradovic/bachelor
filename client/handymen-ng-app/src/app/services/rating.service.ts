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

  getUnverifiedRatings(): Observable<any> {
    return this.http.get('https://localhost:5001/api/ratings/get-unverified-ratings', {headers: this.headers, responseType: 'json'});
  }

  deleteRating($event: number): Observable<any> {
    return this.http.delete('https://localhost:5001/api/ratings/delete-rating/' + $event, {headers: this.headers, responseType: 'json'});
  }

  verifyRating($event: number): Observable<any> {
    return this.http.put('https://localhost:5001/api/ratings/verify-rating/' + $event, null, {headers: this.headers, responseType: 'json'});
  }
}
