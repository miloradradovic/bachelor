import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {StorageService} from '../services/storage.service';
import {LogInData} from '../model/login.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private headers = new HttpHeaders({'Content-Type': 'application/json'});

  constructor(private http: HttpClient, private storageService: StorageService
  ) {
  }

  logIn(auth: LogInData): Observable<any> {
    return this.http.post('https://localhost:5001/api/auth/log-in',
      auth, {headers: this.headers, responseType: 'json'});
  }

  logOut(): void {
    this.storageService.clearStorage();

  }

  getRole(): string {
    if (!sessionStorage.getItem('user')) {
      return '';
    }
    return JSON.parse(sessionStorage.getItem('user')).role;
  }
}
