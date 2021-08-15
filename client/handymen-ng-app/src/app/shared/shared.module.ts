import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {NavbarComponent} from './navbar/navbar.component';
import {NavbarHandymanComponent} from './navbar/navbar-handyman/navbar-handyman.component';
import {NavbarNonSignedInComponent} from './navbar/navbar-non-signed-in/navbar-non-signed-in.component';
import {NavbarUserComponent} from './navbar/navbar-user/navbar-user.component';
import {MaterialModule} from '../material/material.module';
import {AgmCoreModule} from '@agm/core';
import { MapComponent } from './map/map.component';
import {RouterModule} from '@angular/router';



@NgModule({
  declarations: [
    NavbarComponent,
    NavbarHandymanComponent,
    NavbarNonSignedInComponent,
    NavbarUserComponent,
    MapComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    AgmCoreModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyA299mClrC7nDZzy92CQ4X47y7FmaBKMj4'
    }),
    RouterModule
  ],
  exports: [
    NavbarComponent,
    NavbarHandymanComponent,
    NavbarNonSignedInComponent,
    NavbarUserComponent,
    AgmCoreModule,
    MapComponent
  ]
})
export class SharedModule { }
