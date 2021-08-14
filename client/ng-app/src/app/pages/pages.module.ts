import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { JwBootstrapSwitchNg2Module } from 'jw-bootstrap-switch-ng2';
import { NouisliderModule } from 'ng2-nouislider';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { BrowserModule } from '@angular/platform-browser';
import { RegisteringDecideComponent } from './login/registering-decide/registering-decide.component';
import {MaterialModule} from '../material/material.module';
import { RegisterUserComponent } from './register/register-user/register-user.component';
import { RegisterHandymanComponent } from './register/register-handyman/register-handyman.component';
import {ComponentsModule} from '../components/components.module';




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
        ComponentsModule
      ],
    declarations: [
        LoginComponent,
        RegisterComponent,
        RegisteringDecideComponent,
        RegisterUserComponent,
        RegisterHandymanComponent
    ],
    exports: [  ]
})
export class PagesModule { }
