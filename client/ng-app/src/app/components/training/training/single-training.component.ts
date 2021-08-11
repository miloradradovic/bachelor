import { Component, Input, OnInit } from '@angular/core';
import { TrainingModel } from 'app/model/training.model';

@Component({
  selector: 'app-single-training',
  templateUrl: './single-training.component.html',
  styleUrls: ['./single-training.component.css']
})
export class SingleTrainingComponent implements OnInit {

  @Input() model: TrainingModel;

  constructor() { }

  ngOnInit(): void {
  }

}
