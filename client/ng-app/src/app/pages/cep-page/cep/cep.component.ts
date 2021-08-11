import { Component, Input, OnInit } from '@angular/core';
import { MatSnackBar, MAT_SNACK_BAR_DEFAULT_OPTIONS_FACTORY } from '@angular/material/snack-bar';
import { DietModel } from 'app/model/diet.model';
import { TrainingModel } from 'app/model/training.model';
import { TrainingService } from 'app/services/training.service';
import { Observable } from 'rxjs';
import { interval } from 'rxjs';
import * as Rellax from 'rellax';



@Component({
  selector: 'app-cep',
  templateUrl: './cep.component.html',
  styleUrls: ['./cep.component.css']
})
export class CepComponent implements OnInit {

  private trainings: TrainingModel[];

  trainingRunnning: boolean = false;
  cepTry: number = 0
  currentExercise: number = -1;
  currentSession: number = -1;

  private subscription;

  constructor(private trainingService: TrainingService,private _snack : MatSnackBar) { }

  ngOnInit(): void {
    this.trainingService.getMyTrainings().toPromise().then( res => {
      this.trainings = res['trainings'];
    })

    var rellaxHeader = new Rellax('.rellax-header');

        var body = document.getElementsByTagName('body')[0];
        body.classList.add('profile-page');
        var navbar = document.getElementsByTagName('nav')[0];
        navbar.classList.add('navbar-transparent');
  }
  
    ngOnDestroy(){
        var body = document.getElementsByTagName('body')[0];
        body.classList.remove('profile-page');
        var navbar = document.getElementsByTagName('nav')[0];
        navbar.classList.remove('navbar-transparent');
  }


  startTraining(trainingId: number, currentSessionId: number){
    this.currentSession = currentSessionId;
    this.trainingRunnning = true;
    let heartRate = Math.random()*200;
    this.trainingService.cep(heartRate, trainingId)
    this.subscription = interval(1000).subscribe(x => { // will execute every 30 seconds
      this.trainingService.cep(Math.random()*200,trainingId).toPromise().then( res => {
        this.cepTry++;
        if(!res){
          return;
        }
        if(this.cepTry > 20){
          this.cepTry = 0;
          this.stopTraining();
        }
        if (res['type'] === 3){
          this._snack.open("Dangerous exercise, forbidden. You almost died last time.", "Ok");
        }
        if (res['type'] === 2){
          this._snack.open("Alarmingly low heart rate, stopping exercise!!!", "Ok");
        }
        if (res['type'] === 1 ){
          this._snack.open("Alarmingly high heart rate, stopping exercise!!!", "Ok");
        }
        this.stopTraining();
        this.trainingRunnning = false;

      })
    });
  }

  stopTraining(){
    this.currentSession = -1;
    this.trainingRunnning = false;
    this.subscription.unsubscribe();
  }

  
  getUsername(){
    return localStorage["user"]["username"] || 'Vukasin';
  }

}


