import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { ComponentsComponent } from './components/components.component';
import { LandingComponent } from './examples/landing/landing.component';
import { LoginComponent } from './pages/login/login.component';
import { ProfileComponent } from './examples/profile/profile.component';
import { NucleoiconsComponent } from './components/nucleoicons/nucleoicons.component';
import { LandingPageComponent } from './pages/landing-page/landing-page.component';
import { RegisterComponent } from './pages/register/register.component';
import { TrainingComponent } from './pages/training-page/training.component';
import { DietComponent } from './pages/diet-page/diet.component';
import { SearchDietComponent } from './pages/search-diet/search-diet/search-diet.component';
import { CepComponent } from './pages/cep-page/cep/cep.component';
import { AddExerciseComponent } from './pages/add-exercise-page/add-exercise.component';
import { AddMealComponent } from './pages/add-meal-page/add-meal.component';

const routes: Routes =[
    { path: '',                     component: LandingPageComponent },
    { path: 'signup',               component: RegisterComponent },
    { path: 'login',                component: LoginComponent },
    { path: 'training',             component: TrainingComponent },
    { path: 'diet',                 component: DietComponent },
    { path: 'search-diet',          component: SearchDietComponent },
    { path: 'cep',                  component: CepComponent },
    { path: 'index',                component: ComponentsComponent },
    { path: 'add-training',         component: AddExerciseComponent },
    { path: 'add-meal',             component: AddMealComponent },
    { path: 'nucleoicons',          component: NucleoiconsComponent },
    { path: 'examples/landing',     component: LandingComponent },
    { path: 'examples/login',       component: LoginComponent },
    { path: 'examples/profile',     component: ProfileComponent }
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
