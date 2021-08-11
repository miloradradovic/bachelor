import { Component, Input, OnInit } from '@angular/core';
import { DietModel, MealModel } from 'app/model/diet.model';
import { TrainingModel } from 'app/model/training.model';

@Component({
  selector: 'app-meal',
  templateUrl: './meal.component.html',
  styleUrls: ['./meal.component.css']
})
export class MealComponent implements OnInit {

  @Input() model: DietModel;

  constructor() { }

  ngOnInit(): void {
  }

}
