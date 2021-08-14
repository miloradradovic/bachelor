import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {BrowserModule} from '@angular/platform-browser';
import {CommonModule} from '@angular/common';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {NouisliderModule} from 'ng2-nouislider';
import {RouterModule} from '@angular/router';
import {JwBootstrapSwitchNg2Module} from 'jw-bootstrap-switch-ng2';
import {HttpClientModule} from '@angular/common/http';
import {NgxSpinnerModule} from 'ngx-spinner';
import {NavbarComponent} from './navbar/navbar.component';
import {MaterialModule} from '../material/material.module';
import { MapComponent } from './map/map.component';
import {AgmCoreModule} from '@agm/core';

@NgModule({
    imports: [
        ReactiveFormsModule,
        BrowserModule,
        CommonModule,
        FormsModule,
        NgbModule,
        NouisliderModule,
        RouterModule,
        JwBootstrapSwitchNg2Module,
        HttpClientModule,
        NgxSpinnerModule,
        MaterialModule,
        AgmCoreModule.forRoot({
            apiKey: 'AIzaSyA299mClrC7nDZzy92CQ4X47y7FmaBKMj4'
        })
    ],
    declarations: [
        NavbarComponent,
        MapComponent
    ],
    exports: [ NavbarComponent, MapComponent ]
})
export class ComponentsModule { }
