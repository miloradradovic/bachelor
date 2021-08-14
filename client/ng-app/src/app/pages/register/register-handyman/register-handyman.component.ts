import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';
import {MatSnackBar} from '@angular/material/snack-bar';
import {LocationModel} from '../../../model/location.model';
import {TradeService} from '../../../services/trade.service';
import {RegisterUserModel} from '../../../model/register-user.model';
import {RegisterHandymanModel} from '../../../model/register-handyman.model';
import {HandymanService} from '../../../services/handyman.service';

@Component({
  selector: 'app-register-handyman',
  templateUrl: './register-handyman.component.html',
  styleUrls: ['./register-handyman.component.css']
})
export class RegisterHandymanComponent implements OnInit, OnDestroy {

  focus1;
  focus2;
  focus3;
  focus4;
  focus5;
  focus6;
  form: FormGroup;
  private fb: FormBuilder;
  selectedTrades = new FormControl();
  trades = []
  currentLocation: LocationModel = new LocationModel(45.259452102126545, 19.848492145538334, 'Aleksandra Tisme 3, 21101 Novi Sad City, Serbia', 0);


  constructor(
      fb: FormBuilder,
      public router: Router,
      private spinnerService: NgxSpinnerService,
      private snackBar: MatSnackBar,
      private tradeService: TradeService,
      private handymanService: HandymanService
  ) {
    this.fb = fb;
    this.form = this.fb.group({
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.minLength(8)]],
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      address: ['Aleksandra Tisme 3, 21101 Novi Sad City, Serbia', [Validators.required]],

    });
  }

  ngOnInit(): void {
    const body = document.getElementsByTagName('body')[0];
    body.classList.add('login-page');

    const navbar = document.getElementsByTagName('nav')[0];
    navbar.classList.add('navbar-transparent');

    this.getTrades();

  }

  getTrades() {
    this.tradeService.getTrades().subscribe(
        result => {
          this.trades = result.responseObject;
        },
        error => {
          this.snackBar.open(error.message, 'Ok', {duration: 3000});
        }
    );
  }

  registerHandyman() {
    this.spinnerService.show();
    const registrationData: RegisterHandymanModel = new RegisterHandymanModel(
        this.form.value.firstName,
        this.form.value.lastName,
        this.form.value.email,
        this.form.value.password,
        false,
        this.currentLocation,
        this.selectedTrades.value);
    console.log(registrationData);

    this.handymanService.register(registrationData).subscribe(
        result => {
          this.spinnerService.hide();
          this.snackBar.open(result.message, 'Ok', {duration: 5000});
          this.router.navigate(['/']);
        },
        error => {
          this.snackBar.open(error.message, 'Ok', {duration: 3000});
          this.spinnerService.hide();
        }
    );
  }

  ngOnDestroy(): void {
    const body = document.getElementsByTagName('body')[0];
    body.classList.remove('login-page');

    const navbar = document.getElementsByTagName('nav')[0];
    navbar.classList.remove('navbar-transparent');
  }

  dragEnd(locationModel: LocationModel) {
    this.currentLocation.latitude = locationModel.latitude;
    this.currentLocation.longitude = locationModel.longitude;
    this.currentLocation.name = locationModel.name;
    this.form.controls.address.setValue(locationModel.name);
  }

  radiusChanged($event: number) {
    this.currentLocation.radius = $event;
  }
}
