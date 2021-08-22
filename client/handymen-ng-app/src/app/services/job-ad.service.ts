import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {JobAdModel} from '../model/job-ad.model';

@Injectable({
  providedIn: 'root'
})
export class JobAdService {

  private headers = new HttpHeaders({'Content-Type': 'application/json'});

  constructor(private http: HttpClient) { }

  createJobAd(jobAd: JobAdModel) : Observable<any> {
    return this.http.post('https://localhost:5001/api/job-ads/create-job-ad', jobAd, {headers: this.headers, responseType: 'json'});
  }

  getJobAdsByUser(): Observable<any> {
    return this.http.get('https://localhost:5001/api/job-ads/get-job-ads-by-user', {headers: this.headers, responseType: 'json'});
  }

  getJobAdsByHandyman(): Observable<any> {
    return this.http.get('https://localhost:5001/api/job-ads/get-job-ads-by-handyman', {headers: this.headers, responseType: 'json'});
  }

  getJobAdsWithNoOfferByUser(): Observable<any> {
    return this.http.get('https://localhost:5001/api/job-ads/get-job-ads-by-user-no-offer', {headers: this.headers, responseType: 'json'})
  }
}
