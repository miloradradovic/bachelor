import {AppComponent} from './app.component';
import {RouterModule, Routes} from '@angular/router';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {LoginComponent} from './pages/login/login.component';
import {RegisterUserComponent} from './pages/registration/register-user/register-user.component';
import {RegisterHandymanComponent} from './pages/registration/register-handyman/register-handyman.component';

const routes: Routes = [

  {
    path: '',
    component: LoginComponent
  },
  {
    path: 'register-user',
    component: RegisterUserComponent
  },
  {
    path: 'register-handyman',
    component: RegisterHandymanComponent
  }
];

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ],
})
export class AppRoutingModule { }
