import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {LocationModel} from '../../../model/location.model';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';
import {MatSnackBar} from '@angular/material/snack-bar';
import {TradeService} from '../../../services/trade.service';
import {HandymanService} from '../../../services/handyman.service';
import {ProfessionService} from '../../../services/profession.service';
import {CategoryService} from '../../../services/category.service';
import {MatSelectChange} from '@angular/material/select';
import {ProfileDataModel} from '../../../model/profile-data.model';
import {AuthService} from '../../../services/auth.service';

@Component({
  selector: 'app-handyman-profile',
  templateUrl: './handyman-profile.component.html',
  styleUrls: ['./handyman-profile.component.css']
})
export class HandymanProfileComponent implements OnInit {

  form: FormGroup;
  trades = [];
  professions = [];
  categories = [];
  currentLocation: LocationModel = new LocationModel(45.259452102126545, 19.848492145538334, 'Aleksandra Tisme 3, 21101 Novi Sad City, Serbia', 0);
  currentProfession = null;
  currentCategory = null;
  currentTrades = [];
  currentId = 0;
  private fb: FormBuilder;

  constructor(
    fb: FormBuilder,
    public router: Router,
    private spinnerService: NgxSpinnerService,
    private snackBar: MatSnackBar,
    private tradeService: TradeService,
    private handymanService: HandymanService,
    private professionService: ProfessionService,
    private categoryService: CategoryService,
    private authService: AuthService
  ) {
    this.fb = fb;
    this.form = this.fb.group({
      email: [null, [Validators.required, Validators.email]],
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      address: ['Aleksandra Tisme 3, 21101 Novi Sad City, Serbia', [Validators.required]],
      category: [null],
      profession: [null],
      selectedTrades: [[]],
      averageRate: [null, Validators.required]
    });
  }

  ngOnInit(): void {
    this.getCurrentHandyman();
    this.getCategories();
  }

  selectionChange($event: MatSelectChange, type: string): void {
    if (type === 'category') {
      this.getProfessionsByCategory($event.value);
      this.trades = [];
    }
    if (type === 'profession') {
      this.getTradesByProfession($event.value);
    }
  }

  getCurrentHandyman(): void {
    this.handymanService.getCurrentHandyman().subscribe(
      result => {
        this.form.controls.email.setValue(result.responseObject.email);
        this.form.controls.firstName.setValue(result.responseObject.firstName);
        this.form.controls.lastName.setValue(result.responseObject.lastName);
        this.form.controls.address.setValue(result.responseObject.location.name);
        this.form.controls.averageRate.setValue(result.responseObject.averageRate);
        this.currentLocation = new LocationModel(result.responseObject.location.latitude, result.responseObject.location.longitude,
          result.responseObject.location.name, result.responseObject.location.radius);
        this.currentTrades = result.responseObject.trades;
        this.currentId = result.responseObject.id;
      }, error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  getCategories(): void {
    this.categoryService.getCategories().subscribe(
      result => {
        this.categories = result.responseObject;
      },
      error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  getProfessionsByCategory(categoryId: number): void {
    this.professionService.getProfessionsByCategory(categoryId).subscribe(
      result => {
        this.professions = result.responseObject;
      },
      error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  getTradesByProfession(professionId: number): void {
    this.tradeService.getTradesByProfession(professionId).subscribe(
      result => {
        this.trades = result.responseObject;
        this.form.controls.selectedTrades.setValue(this.currentTrades);
      },
      error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  dragEnd(locationModel: LocationModel): void {
    this.currentLocation = locationModel;
    this.form.controls.address.setValue(locationModel.name);
  }

  radiusChanged(locationModel: LocationModel): void {
    this.currentLocation = locationModel;
  }

  editProfile(): void {
    let selectedTrades = [];
    if (this.form.value.selectedTrades === []) {
      selectedTrades = this.currentTrades;
    } else {
      selectedTrades = this.form.value.selectedTrades;
    }
    this.spinnerService.show();
    const profileData: ProfileDataModel = new ProfileDataModel(
      this.currentId,
      this.form.value.firstName,
      this.form.value.lastName,
      this.form.value.email,
      this.currentLocation,
      selectedTrades
    );
    this.handymanService.editProfile(profileData).subscribe(
      result => {
        this.spinnerService.hide();
        this.snackBar.open(result.message, 'Ok', {duration: 3000});
        this.authService.logOut();
        this.router.navigate(['/']);
      }, error => {
        this.spinnerService.hide();
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }
}
