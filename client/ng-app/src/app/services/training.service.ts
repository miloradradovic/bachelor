import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { ExerciseModel, InputDataTraining } from 'app/model/training.model';

@Injectable({
  providedIn: 'root'
})
export class TrainingService {
  private headers = new HttpHeaders({'Content-Type': 'application/json'});

  constructor(private http: HttpClient
  ) {
  }

  search = (trainingInput: InputDataTraining) => this.http.post("http://localhost:8080/training", trainingInput);

  cep = (heartRate: number, exerciseDTO: number) => this.http.post("http://localhost:8080/training/cep", {"heartRate": heartRate, "exerciseDTO": exerciseDTO});

  getMyTrainings = () => this.http.get("http://localhost:8080/training/get-plan-by-logged-in-user");

  addExercise = (exercise: ExerciseModel) => this.http.post("http://localhost:8080/exercises", exercise);


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
