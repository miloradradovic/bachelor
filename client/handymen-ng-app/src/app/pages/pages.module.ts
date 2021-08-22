import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {LoginComponent} from './login/login.component';
import {RegisterHandymanComponent} from './registration/register-handyman/register-handyman.component';
import {RegisterUserComponent} from './registration/register-user/register-user.component';
import {RegisteringDecideComponent} from './dialogs/registering-decide/registering-decide.component';
import {SharedModule} from '../shared/shared.module';
import {MaterialModule} from '../material/material.module';
import {NgxSpinnerModule} from 'ngx-spinner';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HandymenDashboardComponent } from './user-pages/handymen-dashboard/handymen-dashboard.component';
import { JobAdDashboardComponent } from './handyman-pages/job-ad-dashboard/job-ad-dashboard.component';
import { AdminLandingPageComponent } from './admin-pages/admin-landing-page/admin-landing-page.component';
import { CreateJobadFormComponent } from './user-pages/create-jobad-form/create-jobad-form.component';
import { JobadDashboardComponent } from './user-pages/jobad-dashboard/jobad-dashboard.component';
import { CreateInterestDialogComponent } from './dialogs/create-interest-dialog/create-interest-dialog.component';
import { InterestsDashboardComponent } from './user-pages/interests-dashboard/interests-dashboard.component';
import { JobsDashboardComponent } from './user-pages/jobs-dashboard/jobs-dashboard.component';
import { JobDashboardComponent } from './handyman-pages/job-dashboard/job-dashboard.component';
import { RatingFormComponent } from './dialogs/rating-form/rating-form.component';
import {NgxStarRatingModule} from 'ngx-star-rating';
import { DetailedHandymanDialogComponent } from './dialogs/detailed-handyman-dialog/detailed-handyman-dialog.component';
import { SelectJobAdDialogComponent } from './dialogs/select-job-ad-dialog/select-job-ad-dialog.component';
import { OfferDashboardComponent } from './handyman-pages/offer-dashboard/offer-dashboard.component';



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
    CreateInterestDialogComponent,
    InterestsDashboardComponent,
    JobsDashboardComponent,
    JobDashboardComponent,
    RatingFormComponent,
    DetailedHandymanDialogComponent,
    SelectJobAdDialogComponent,
    OfferDashboardComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    MaterialModule,
    NgxSpinnerModule,
    ReactiveFormsModule,
    NgxStarRatingModule,
    FormsModule
  ],
  exports: [
    LoginComponent,
    RegisterHandymanComponent,
    RegisterUserComponent,
    RegisteringDecideComponent
  ]
})
export class PagesModule { }
