import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {LoginComponent} from './login/login.component';
import {RegisterHandymanComponent} from './registration/register-handyman/register-handyman.component';
import {RegisterUserComponent} from './registration/register-user/register-user.component';
import {RegisteringDecideComponent} from './login/registering-decide/registering-decide.component';
import {SharedModule} from '../shared/shared.module';
import {MaterialModule} from '../material/material.module';
import {NgxSpinnerModule} from 'ngx-spinner';
import {ReactiveFormsModule} from '@angular/forms';
import { HandymenDashboardComponent } from './user-pages/handymen-dashboard/handymen-dashboard.component';
import { JobAdDashboardComponent } from './handyman-pages/job-ad-dashboard/job-ad-dashboard.component';
import { AdminLandingPageComponent } from './admin-pages/admin-landing-page/admin-landing-page.component';
import { CreateJobadFormComponent } from './user-pages/create-jobad-form/create-jobad-form.component';
import { JobadDashboardComponent } from './user-pages/jobad-dashboard/jobad-dashboard.component';
import { CreateInterestDialogComponent } from './handyman-pages/job-ad-dashboard/create-interest-dialog/create-interest-dialog.component';



@NgModule({
  declarations: [
    LoginComponent,
    RegisterHandymanComponent,
    RegisterUserComponent,
    RegisteringDecideComponent,
    HandymenDashboardComponent,
    JobAdDashboardComponent,
    AdminLandingPageComponent,
    CreateJobadFormComponent,
    JobadDashboardComponent,
    CreateInterestDialogComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    MaterialModule,
    NgxSpinnerModule,
    ReactiveFormsModule
  ],
  exports: [
    LoginComponent,
    RegisterHandymanComponent,
    RegisterUserComponent,
    RegisteringDecideComponent
  ]
})
export class PagesModule { }
