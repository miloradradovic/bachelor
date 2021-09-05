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
import {RegisteringDecideComponent} from '../dialogs/registering-decide/registering-decide.component';
import {LoggedInModel} from '../../model/logged-in.model';

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
        const info = jwt.decodeToken(result.responseObject);
        const loggedIn = new LoggedInModel(info.id, info.email, info.role, result.responseObject);
        this.storageService.setStorageItem('user', JSON.stringify(loggedIn));
        this.spinnerService.hide();
        this.snackBar.open(result.message, 'Ok', {duration: 2000});
        if(loggedIn.role === 'USER') {
          this.router.navigate(['/user/handymen-dashboard-browse']);
        } else if(loggedIn.role === 'HANDYMAN') {
          this.router.navigate(['/handyman/jobad-dashboard']);
        } else {
          this.router.navigate(['/admin/registration-requests']);
        }

      },
      error => {
        this.spinnerService.hide();
        this.snackBar.open(error.error.message, 'Ok', {duration: 2000});
      }
    );
  }

  createAccount() {
    const dialogRef = this.dialog.open(RegisteringDecideComponent, {
      width: '30%'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result === 'user') {
        this.router.navigate(['/user/register']);
      } else if (result === 'handyman') {
        this.router.navigate(['/handyman/register']);
      }
    });
  }
}
