import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { InputDataTraining } from 'app/model/training.model';
import { DataInputModel, MealModel } from 'app/model/diet.model';
import { QueryModel } from 'app/model/query.model';

@Injectable({
  providedIn: 'root'
})
export class DietService {
  private headers = new HttpHeaders({'Content-Type': 'application/json'});

  constructor(private http: HttpClient
  ) {
  }

  search = (dietInput: DataInputModel) => this.http.post("http://localhost:8080/diet", dietInput);

  searchDiet = (searchModel: QueryModel) => this.http.post("http://localhost:8080/diet/searchMeals", searchModel);

  addMeal = (mealModel: MealModel) => this.http.post("http://localhost:8080/meals", mealModel);


/*
  logIn(auth: LogIn): Observable<any> {
    return this.http.post('http://localhost:8080/auth/log-in',
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
  }*/
}
