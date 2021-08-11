import { Component, Input, OnInit } from '@angular/core';
import { MatSnackBar, MAT_SNACK_BAR_DEFAULT_OPTIONS_FACTORY } from '@angular/material/snack-bar';
import { DietModel } from 'app/model/diet.model';
import { TrainingModel } from 'app/model/training.model';
import { TrainingService } from 'app/services/training.service';
import { Observable } from 'rxjs';
import { interval } from 'rxjs';
import * as Rellax from 'rellax';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DietService } from 'app/services/diet.service';



@Component({
  selector: 'app-add-meal',
  templateUrl: './add-meal.component.html',
  styleUrls: ['./add-meal.component.css']
})
export class AddMealComponent implements OnInit {

  private trainings: TrainingModel[];


  healthIssues: String[] = ["DIABETES",
    "CHOLESTEROL",
    "HIGH_FAT",
    "TRIGLYCERIDES",
    "HEART_PROBLEMS"]

  trainingRunnning: boolean = false;
  cepTry: number = 0
  currentExercise: number = -1;
  currentSession: number = -1;
  inputForm: FormGroup;

  private subscription;

  constructor(private dietService: DietService,private _snack : MatSnackBar, private fb: FormBuilder) {
    this.inputForm = fb.group({
      'kCal': 0,
      'carbonHydrates': 0,
      'healthIssueTypes': [[]],
      'proteins': 0,
      'fats': 0,
      'name': '',
      'mealType': '',
      'junkPercentage': 0,
      'recipe': ''
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

  addMeal(){
    this.dietService.addMeal(this.inputForm.value).toPromise().then( res => {
      this._snack.open("Successfully added.", "Close");
    }, err => {
      this._snack.open("Adding failed.", "Close");
    })
  }

  getUsername(){
    return localStorage["user"]["username"] || 'Admin';
  }

}


