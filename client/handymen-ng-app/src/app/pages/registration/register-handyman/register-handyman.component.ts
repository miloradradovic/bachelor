import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {LocationModel} from '../../../model/location.model';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';
import {MatSnackBar} from '@angular/material/snack-bar';
import {TradeService} from '../../../services/trade.service';
import {HandymanService} from '../../../services/handyman.service';
import {RegisterDataModel} from '../../../model/register-data.model';
import {ProfessionService} from '../../../services/profession.service';
import {CategoryService} from '../../../services/category.service';
import {MatSelectChange} from '@angular/material/select';

@Component({
  selector: 'app-register-handyman',
  templateUrl: './register-handyman.component.html',
  styleUrls: ['./register-handyman.component.css']
})
export class RegisterHandymanComponent implements OnInit {

  form: FormGroup;
  private fb: FormBuilder;
  trades = []
  professions = []
  categories = []
  currentLocation: LocationModel = new LocationModel(45.259452102126545, 19.848492145538334, 'Aleksandra Tisme 3, 21101 Novi Sad City, Serbia', 0);

  constructor(
    fb: FormBuilder,
    public router: Router,
    private spinnerService: NgxSpinnerService,
    private snackBar: MatSnackBar,
    private tradeService: TradeService,
    private handymanService: HandymanService,
    private professionService: ProfessionService,
    private categoryService: CategoryService
  ) {
    this.fb = fb;
    this.form = this.fb.group({
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.minLength(8)]],
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      address: ['Aleksandra Tisme 3, 21101 Novi Sad City, Serbia', [Validators.required]],
      category: [null, [Validators.required]],
      profession: [null, [Validators.required]],
      selectedTrades: [[], [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.getCategories();
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

  registerHandyman() {
    this.spinnerService.show();
    const registrationData: RegisterDataModel = new RegisterDataModel(
      this.form.value.firstName,
      this.form.value.lastName,
      this.form.value.email,
      this.form.value.password,
      false,
      this.currentLocation,
      this.form.value.selectedTrades);
    this.handymanService.register(registrationData).subscribe(
      result => {
        this.spinnerService.hide();
        this.snackBar.open(result.message, 'Ok', {duration: 5000});
        this.router.navigate(['/']);
        },
      error => {
        this.spinnerService.hide();
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      });
  }

  dragEnd(locationModel: LocationModel) {
    this.currentLocation = locationModel;
    this.form.controls.address.setValue(locationModel.name);
  }

  radiusChanged(locationModel: LocationModel) {
    this.currentLocation = locationModel
  }

}
