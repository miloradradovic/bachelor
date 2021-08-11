import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {StorageService} from '../services/storage.service';
import {LogIn, RegisterModel, UserRole} from '../model/login.model';

@Injectable({
  providedIn: 'root'
})
export class LogInService {
  private headers = new HttpHeaders({'Content-Type': 'application/json'});

  constructor(private http: HttpClient, private storageService: StorageService
  ) {
  }

  logIn(auth: LogIn): Observable<any> {
    return this.http.post('https://localhost:5001/api/auth/log-in',
      auth, {headers: this.headers, responseType: 'json'});
  }

  logOut(): void {
    this.http.post('http://localhost:8080/auth/log-out', {headers: this.headers}).toPromise().then( success => {
        this.storageService.clearStorage();
    });
    
  }

  register(regModel: RegisterModel): Observable<any> {
    return this.http.post('http://localhost:8080/auth/register', regModel, {headers: this.headers});
  }

  getRole(): UserRole {
    if (!localStorage.getItem('user')) {
      return UserRole.UNAUTHORIZED;
    }
    return JSON.parse(localStorage.getItem('user')).role === 'ADMIN' ? UserRole.ADMIN : UserRole.UNAUTHORIZED;
  }
}
