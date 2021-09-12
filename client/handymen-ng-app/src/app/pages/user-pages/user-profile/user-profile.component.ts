import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {LocationModel} from '../../../model/location.model';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';
import {MatSnackBar} from '@angular/material/snack-bar';
import {TradeService} from '../../../services/trade.service';
import {ProfessionService} from '../../../services/profession.service';
import {CategoryService} from '../../../services/category.service';
import {AuthService} from '../../../services/auth.service';
import {ProfileDataModel} from '../../../model/profile-data.model';
import {UserService} from '../../../services/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  form: FormGroup;
  currentLocation: LocationModel = new LocationModel(45.259452102126545, 19.848492145538334, 'Aleksandra Tisme 3, 21101 Novi Sad City, Serbia', 0);
  currentId = 0;
  private fb: FormBuilder;

  constructor(
    fb: FormBuilder,
    public router: Router,
    private spinnerService: NgxSpinnerService,
    private snackBar: MatSnackBar,
    private tradeService: TradeService,
    private userService: UserService,
    private professionService: ProfessionService,
    private categoryService: CategoryService,
    private authService: AuthService
  ) {
    this.fb = fb;
    this.form = this.fb.group({
      email: [null, [Validators.required, Validators.email]],
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      address: ['Aleksandra Tisme 3, 21101 Novi Sad City, Serbia', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.getCurrentUser();
  }

  getCurrentUser(): void {
    this.userService.getCurrentUser().subscribe(
      result => {
        this.form.controls.email.setValue(result.responseObject.email);
        this.form.controls.firstName.setValue(result.responseObject.firstName);
        this.form.controls.lastName.setValue(result.responseObject.lastName);
        this.form.controls.address.setValue(result.responseObject.location.name);
        this.currentLocation = new LocationModel(result.responseObject.location.latitude, result.responseObject.location.longitude,
          result.responseObject.location.name, result.responseObject.location.radius);
        this.currentId = result.responseObject.id;
      }, error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  dragEnd(locationModel: LocationModel): void {
    this.currentLocation = locationModel;
    this.form.controls.address.setValue(locationModel.name);
  }

  editProfile(): void {
    this.spinnerService.show();
    const profileData: ProfileDataModel = new ProfileDataModel(
      this.currentId,
      this.form.value.firstName,
      this.form.value.lastName,
      this.form.value.email,
      this.currentLocation,
      []
    );
    this.userService.editProfile(profileData).subscribe(
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
