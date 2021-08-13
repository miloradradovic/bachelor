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
import {MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatCardModule} from '@angular/material/card';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {NavbarComponent} from './navbar/navbar.component';

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
        MatSelectModule,
        MatInputModule,
        MatCheckboxModule,
        MatCardModule,
        MatSnackBarModule,
        MatInputModule
    ],
    declarations: [
        NavbarComponent
    ],
    exports: [ NavbarComponent ]
})
export class ComponentsModule { }
