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
import { NavbarAdminComponent } from './navbar/navbar-admin/navbar-admin.component';
import { TableComponent } from './table/table.component';
import {MatPaginatorModule} from '@angular/material/paginator';



@NgModule({
  declarations: [
    NavbarComponent,
    NavbarHandymanComponent,
    NavbarNonSignedInComponent,
    NavbarUserComponent,
    MapComponent,
    NavbarAdminComponent,
    TableComponent
  ],
    imports: [
        CommonModule,
        MaterialModule,
        AgmCoreModule,
        AgmCoreModule.forRoot({
            apiKey: 'AIzaSyA299mClrC7nDZzy92CQ4X47y7FmaBKMj4'
        }),
        RouterModule,
        MatPaginatorModule
    ],
  exports: [
    NavbarComponent,
    NavbarHandymanComponent,
    NavbarNonSignedInComponent,
    NavbarUserComponent,
    AgmCoreModule,
    MapComponent,
    TableComponent
  ]
})
export class SharedModule { }
