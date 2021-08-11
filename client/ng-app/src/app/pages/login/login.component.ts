import {Component, OnDestroy, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LogIn, LogInModel } from 'app/model/login.model';
import {JwtHelperService} from '@auth0/angular-jwt';
import { LogInService } from 'app/services/log-in.service';
import { Router } from '@angular/router';
import { StorageService } from 'app/services/storage.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { MatSnackBar } from '@angular/material/snack-bar';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {

    data: Date = new Date();
    focus;
    focus1;


    form: FormGroup;
    error = '';
    private fb: FormBuilder;


    constructor(
        fb: FormBuilder,
        private logInService: LogInService,
        public router: Router,
        private storageService: StorageService,
        private spinnerService: NgxSpinnerService,
        private snackBar: MatSnackBar
      ) {
        this.fb = fb;
        this.form = this.fb.group({
          email: [null, [Validators.required, Validators.email]],
          password: [null, Validators.required]
        });
      }

    ngOnInit() {
        const body = document.getElementsByTagName('body')[0];
        body.classList.add('login-page');

        const navbar = document.getElementsByTagName('nav')[0];
        navbar.classList.add('navbar-transparent');
    }

    ngOnDestroy() {
        const body = document.getElementsByTagName('body')[0];
        body.classList.remove('login-page');

        const navbar = document.getElementsByTagName('nav')[0];
        navbar.classList.remove('navbar-transparent');
    }

    submit(): void {
        const logIn: LogIn = this.form.value;
        console.log(logIn);
        this.spinnerService.show();
        this.logInService.logIn(logIn).subscribe(
          result => {
              console.log(result);
            const jwt: JwtHelperService = new JwtHelperService();
            const info = jwt.decodeToken(result.accessToken);
            /*
            const role = info.role;
            const user = new LogInModel(info.username, result.accessToken ,info.id, info.role);
            localStorage.setItem('user', JSON.stringify(user));
            this.storageService.setStorageItem('user', JSON.stringify(user))
             */
              this.spinnerService.hide();
            this.snackBar.open(result.message, 'Ok', {duration: 2000});
            /*
            if (role === 'ROLE_USER' || role === 'ROLE_ADMINISTRATOR') {
                this.spinnerService.hide();
                this.router.navigate(['/']);
            }
             */


          },
          error => {
              console.log(error)
            this.snackBar.open(error.message, 'Ok', {duration: 2000});
            this.spinnerService.hide();
          }
        );


      }

}
