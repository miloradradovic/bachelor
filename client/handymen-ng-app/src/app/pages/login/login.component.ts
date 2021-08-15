import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MatSnackBar} from '@angular/material/snack-bar';
import {AuthService} from '../../services/auth.service';
import {ActivatedRoute, Router} from '@angular/router';
import {StorageService} from '../../services/storage.service';
import {NgxSpinnerService} from 'ngx-spinner';
import {MatDialog} from '@angular/material/dialog';
import {UserService} from '../../services/user.service';
import {JwtHelperService} from '@auth0/angular-jwt';
import {LogInData} from '../../model/login.model';
import {RegisteringDecideComponent} from './registering-decide/registering-decide.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form: FormGroup;
  data: Date = new Date();

  constructor(
    private fb: FormBuilder,
    private logInService: AuthService,
    public router: Router,
    private storageService: StorageService,
    private spinnerService: NgxSpinnerService,
    private snackBar: MatSnackBar,
    public dialog: MatDialog,
    private activatedRoute: ActivatedRoute,
    private userService: UserService
  ) {
    this.fb = fb;
    this.form = this.fb.group({
      email: [null, [Validators.required, Validators.email]],
      password: [null, Validators.required]
    });
  }

  ngOnInit(): void {
    const idPassed = this.activatedRoute.snapshot.queryParamMap.get('id');
    if (idPassed) {
      this.spinnerService.show();
      this.userService.verify(idPassed).subscribe(
        result => {
          this.spinnerService.hide();
          this.snackBar.open(result.message, 'Ok', {duration: 3000});
        },
        error => {
          this.snackBar.open(error.message, 'Ok', {duration: 3000});
          this.spinnerService.hide();
        }
      );
    }
  }

  submit() {
    const logIn: LogInData = this.form.value;
    this.spinnerService.show();
    this.logInService.logIn(logIn).subscribe(
      result => {
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
        this.snackBar.open(error.error.message, 'Ok', {duration: 2000});
        this.spinnerService.hide();
      }
    );
  }

  createAccount() {
    const dialogRef = this.dialog.open(RegisteringDecideComponent, {
      width: '30%'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result === 'user') {
        this.router.navigate(['/register-user']);
      } else if (result === 'handyman') {
        this.router.navigate(['/register-handyman']);
      }
    });
  }
}
