import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import {RegisterHandymanComponent} from './pages/register/register-handyman/register-handyman.component';
import {RegisterUserComponent} from './pages/register/register-user/register-user.component';
import {MapComponent} from './components/map/map.component';

const routes: Routes = [

    {
        path: '',
        component: LoginComponent
    },
    {
        path: 'signup',
        component: RegisterComponent
    },
    {
        path: 'register-handyman',
        component: RegisterHandymanComponent
    },
    {
        path: 'register-user',
        component: RegisterUserComponent
    },
    {
        path: 'map',
        component: MapComponent
    }
];

@NgModule({
    imports: [
        CommonModule,
        BrowserModule,
        RouterModule.forRoot(routes)
    ],
    exports: [
    ],
})
export class AppRoutingModule { }
