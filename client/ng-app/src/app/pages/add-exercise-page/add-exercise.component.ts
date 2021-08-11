import { Component, Input, OnInit } from '@angular/core';
import { MatSnackBar, MAT_SNACK_BAR_DEFAULT_OPTIONS_FACTORY } from '@angular/material/snack-bar';
import { DietModel } from 'app/model/diet.model';
import { TrainingModel } from 'app/model/training.model';
import { TrainingService } from 'app/services/training.service';
import { Observable } from 'rxjs';
import { interval } from 'rxjs';
import * as Rellax from 'rellax';
import { FormBuilder, FormGroup } from '@angular/forms';



@Component({
  selector: 'app-add-exercise',
  templateUrl: './add-exercise.component.html',
  styleUrls: ['./add-exercise.component.css']
})
export class AddExerciseComponent implements OnInit {

  private trainings: TrainingModel[];
  muscles: String[] = ["BICEPS",
  "TRICEPS",
  "QUADRICEPS",
  "CALVES",
  "HAMSTRINGS",
  "GLUTES",
  "FOREARMS",
  "TRAPEZIUS",
  "LATS",
  "ABDOMINALS"]

  trainingRunnning: boolean = false;
  cepTry: number = 0
  currentExercise: number = -1;
  currentSession: number = -1;
  inputForm: FormGroup;

  private subscription;

  constructor(private trainingService: TrainingService,private _snack : MatSnackBar, private fb: FormBuilder) {
    this.inputForm = fb.group({
      'name': '',
      'description': '',
      'muscleList': [[]],
      'difficulty': '',
      'equipment': false,
      'exerciseCategory': ''
    })
   }

  ngOnInit(): void {
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

  addExercise(){
    this.trainingService.addExercise(this.inputForm.value).toPromise().then( res => {
      this._snack.open("Successfully added.", "Close");
    }, err => {
      this._snack.open("Adding failed.", "Close");
    })
  }

  getUsername(){
    return localStorage["user"]["username"] || 'Admin';
  }

}


