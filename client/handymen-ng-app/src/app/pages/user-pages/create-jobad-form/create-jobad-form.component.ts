import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {LocationModel} from '../../../model/location.model';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';
import {MatSnackBar} from '@angular/material/snack-bar';
import {TradeService} from '../../../services/trade.service';
import {HandymanService} from '../../../services/handyman.service';
import {ProfessionService} from '../../../services/profession.service';
import {CategoryService} from '../../../services/category.service';
import {MatSelectChange} from '@angular/material/select';
import {StepperSelectionEvent} from '@angular/cdk/stepper';

@Component({
  selector: 'app-create-jobad-form',
  templateUrl: './create-jobad-form.component.html',
  styleUrls: ['./create-jobad-form.component.css']
})
export class CreateJobadFormComponent implements OnInit {

  firstForm: FormGroup;
  secondForm: FormGroup;
  thirdForm: FormGroup;
  fourthForm: FormGroup;
  private fb: FormBuilder;
  trades = []
  professions = []
  categories = []
  currentLocation: LocationModel = new LocationModel(45.259452102126545, 19.848492145538334, 'Aleksandra Tisme 3, 21101 Novi Sad City, Serbia', 0);
  secondFormClicked = false;
  fillOutAdditionalData = false;
  urgents = [{boolVal: true, name: 'Yes'}, {boolVal: false, name: 'No'}];
  fiveDaysAhead: Date;

  constructor(fb: FormBuilder,
              public router: Router,
              private spinnerService: NgxSpinnerService,
              private snackBar: MatSnackBar,
              private tradeService: TradeService,
              private professionService: ProfessionService,
              private categoryService: CategoryService) {
    this.fb = fb;
    this.firstForm = this.fb.group({
      title: [null, [Validators.required]],
      description: [null, [Validators.required]]
    });
    this.secondForm = this.fb.group({
      address: [this.currentLocation.name, Validators.required],
      date: ['', Validators.required]
    });
    this.thirdForm = this.fb.group({
      category: [null, Validators.required],
      profession: [null, Validators.required],
      selectedTrades: [[], Validators.required]
    });
    this.fourthForm = this.fb.group({
      urgent: [null],
      maxPrice: [null]
    })
  }

  ngOnInit(): void {
    this.fiveDaysAhead = new Date();
    this.fiveDaysAhead.setDate(this.fiveDaysAhead.getDate() + 7);
    this.fiveDaysAhead.setMonth(this.fiveDaysAhead.getMonth());
    this.fiveDaysAhead.setFullYear(this.fiveDaysAhead.getFullYear());

  }

  dragEnd(locationModel: LocationModel) {
    this.currentLocation = locationModel;
    this.secondForm.controls.address.setValue(locationModel.name);
  }

  selectionChange($event: MatSelectChange, type: string) {
    if (type === 'category') {
      this.getProfessionsByCategory($event.value);
      this.trades = [];
    }
    if (type === 'profession') {
      this.getTradesByProfession($event.value);
    }
  }

  getCategories() {
    this.categoryService.getCategories().subscribe(
      result => {
        this.categories = result.responseObject;
      },
      error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    )
  }

  getProfessionsByCategory(categoryId: number) {
    this.professionService.getProfessionsByCategory(categoryId).subscribe(
      result => {
        this.professions = result.responseObject;
      },
      error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    )
  }

  getTradesByProfession(professionId: number) {
    this.tradeService.getTradesByProfession(professionId).subscribe(
      result => {
        this.trades = result.responseObject;
      },
      error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  clicked(type: string) {
    if (type === 'firstFormNext') {
      this.secondFormClicked = true;
    } else if (type === 'secondFormBack') {
      this.secondFormClicked = false;
    } else if (type === 'secondFormNext') {
      this.secondFormClicked = false;
      this.getCategories();
    } else if (type === 'thirdFormBack') {
      this.secondFormClicked = true;
    } else if (type === 'additionalData') {
      this.fillOutAdditionalData = true;
    } else if (type === 'additionalDataGiveUp') {
      this.fillOutAdditionalData = false;
    }
  }

  createJobAd() {

  }

  changedMatStep($event: StepperSelectionEvent) {
    if ($event.selectedIndex === 1) {
      this.secondFormClicked = true;
    } else {
      this.secondFormClicked = false;
    }
  }
}
