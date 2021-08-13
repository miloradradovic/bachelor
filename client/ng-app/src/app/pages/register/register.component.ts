import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LogInService } from 'app/services/log-in.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { RegisterModel } from '../../model/login.model'
import {MatSnackBar} from '@angular/material/snack-bar';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  form: FormGroup;
  error = '';
  private fb: FormBuilder;


  constructor(
      fb: FormBuilder,
      public router: Router,
      private spinnerService: NgxSpinnerService,
      private snackBar: MatSnackBar,
      private loginService: LogInService
    ) {
      this.fb = fb;
      this.form = this.fb.group({
        firstName: [null, [Validators.required]],
        lastName: [null, Validators.required],
        username: [null, Validators.required],
        password: [null, Validators.required],
        age: [null, Validators.required]
      });
    }

  ngOnInit() {
      var body = document.getElementsByTagName('body')[0];
      body.classList.add('login-page');

      var navbar = document.getElementsByTagName('nav')[0];
      navbar.classList.add('navbar-transparent');
  }
  ngOnDestroy(){
      var body = document.getElementsByTagName('body')[0];
      body.classList.remove('login-page');

      var navbar = document.getElementsByTagName('nav')[0];
      navbar.classList.remove('navbar-transparent');
  }

  submit(): void {
      const logIn: RegisterModel = this.form.value;
      this.spinnerService.show();
      this.loginService.register(logIn).subscribe(
        result => {
          this.snackBar.open('Bad credentials!', 'Ok', {duration: 2000});
          this.router.navigate(['/']);
        },
        error => {
          this.snackBar.open('Bad credentials!', 'Ok', {duration: 2000});
          this.spinnerService.hide();
        }
      );
      

    }
}
