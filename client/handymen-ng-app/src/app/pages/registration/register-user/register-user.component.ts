import { Component, OnInit } from '@angular/core';
import {LocationModel} from '../../../model/location.model';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';
import {MatSnackBar} from '@angular/material/snack-bar';
import {UserService} from '../../../services/user.service';
import {RegisterUserModel} from '../../../model/register-user.model';
import {RegisterDataModel} from '../../../model/register-data.model';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {

  currentLocation: LocationModel = new LocationModel(45.259452102126545, 19.848492145538334, 'Aleksandra Tisme 3, 21101 Novi Sad City, Serbia', 0);
  form: FormGroup;
  private fb: FormBuilder;
  options = {
    fields: ['latitude', 'longitude', 'name']
  };

  constructor(
    fb: FormBuilder,
    public router: Router,
    private spinnerService: NgxSpinnerService,
    private snackBar: MatSnackBar,
    private userService: UserService
  ) {
    this.fb = fb;
    this.form = this.fb.group({
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.minLength(8)]],
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      address: ['Aleksandra Tisme 3, 21101 Novi Sad City, Serbia', [Validators.required]]
    });
  }

  ngOnInit(): void {
  }

  dragEnd(locationModel: LocationModel) {
    this.currentLocation = locationModel;
    this.currentLocation.radius = 0;
    console.log(locationModel);
    this.form.controls.address.setValue(locationModel.name);

  }

  registerUser() {
    this.spinnerService.show();
    const registrationData: RegisterDataModel = new RegisterDataModel(
      this.form.value.firstName,
      this.form.value.lastName,
      this.form.value.email,
      this.form.value.password,
      false,
      this.currentLocation,
      []);
    this.userService.register(registrationData).subscribe(
      result => {
        this.spinnerService.hide();
        this.snackBar.open(result.message, 'Ok', {duration: 5000});
        this.router.navigate(['/']);
      },
      error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
        this.spinnerService.hide();
      }
    );
  }

  radiusChanged(newRadius: number) {
    this.currentLocation.radius = newRadius;
  }

}
