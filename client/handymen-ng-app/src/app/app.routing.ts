import {AppComponent} from './app.component';
import {RouterModule, Routes} from '@angular/router';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {LoginComponent} from './pages/login/login.component';
import {RegisterUserComponent} from './pages/registration/register-user/register-user.component';
import {RegisterHandymanComponent} from './pages/registration/register-handyman/register-handyman.component';
import {HandymenDashboardComponent} from './pages/user-pages/handymen-dashboard/handymen-dashboard.component';
import {JobAdDashboardComponent} from './pages/handyman-pages/job-ad-dashboard/job-ad-dashboard.component';
import {AdminLandingPageComponent} from './pages/admin-pages/admin-landing-page/admin-landing-page.component';
import {LogInGuard} from './guards/log-in-guard.service';
import {UserGuard} from './guards/user-guard.service';
import {HandymanGuard} from './guards/handyman-guard.service';
import {AdministratorGuard} from './guards/administrator-guard.service';

const routes: Routes = [

  {
    path: '',
    component: LoginComponent,
    canActivate: [LogInGuard]
  },
  {
    path: 'register-user',
    component: RegisterUserComponent,
    canActivate: [LogInGuard]
  },
  {
    path: 'register-handyman',
    component: RegisterHandymanComponent,
    canActivate: [LogInGuard]
  },
  {
    path: 'handymen-dashboard',
    component: HandymenDashboardComponent,
    canActivate: [UserGuard]
  },
  {
    path: 'jobad-dashboard',
    component: JobAdDashboardComponent,
    canActivate: [HandymanGuard]
  },
  {
    path: 'admin-landing-page',
    component: AdminLandingPageComponent,
    canActivate: [AdministratorGuard]
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
