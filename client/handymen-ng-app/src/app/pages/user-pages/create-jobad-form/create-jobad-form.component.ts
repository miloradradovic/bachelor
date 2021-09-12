import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {LocationModel} from '../../../model/location.model';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';
import {MatSnackBar} from '@angular/material/snack-bar';
import {TradeService} from '../../../services/trade.service';
import {ProfessionService} from '../../../services/profession.service';
import {CategoryService} from '../../../services/category.service';
import {MatSelectChange} from '@angular/material/select';
import {StepperSelectionEvent} from '@angular/cdk/stepper';
import {JobAdModel} from '../../../model/job-ad.model';
import {AdditionalJobAdInfoModel} from '../../../model/additional-job-ad-info.model';
import {JobAdService} from '../../../services/job-ad.service';
import {LocationService} from '../../../services/location.service';

@Component({
  selector: 'app-create-jobad-form',
  templateUrl: './create-jobad-form.component.html',
  styleUrls: ['./create-jobad-form.component.css']
})
export class CreateJobadFormComponent implements OnInit {

  firstForm: FormGroup;
  secondForm: FormGroup;
  thirdForm: FormGroup;
  fourthForm: FormGroup;
  trades = [];
  professions = [];
  categories = [];
  currentLocation: LocationModel = new LocationModel(45.259452102126545, 19.848492145538334, 'Aleksandra Tisme 3, 21101 Novi Sad City, Serbia', 0);
  secondFormClicked = false;
  fillOutAdditionalData = false;
  urgents = [{boolVal: true, name: 'Da'}, {boolVal: false, name: 'Ne'}];
  fiveDaysAhead: Date;
  pictures = [];
  @ViewChild('fileInput') fileInput: ElementRef;
  private fb: FormBuilder;

  constructor(fb: FormBuilder,
              public router: Router,
              private spinnerService: NgxSpinnerService,
              private snackBar: MatSnackBar,
              private tradeService: TradeService,
              private professionService: ProfessionService,
              private categoryService: CategoryService,
              private jobAdService: JobAdService,
              private locationService: LocationService) {
    this.fb = fb;
    this.firstForm = this.fb.group({
      title: [null, [Validators.required]],
      description: [null, [Validators.required]]
    });
    this.secondForm = this.fb.group({
      address: [this.currentLocation.name, Validators.required],
      date: ['', Validators.required]
    });
    this.thirdForm = this.fb.group({
      category: [null, Validators.required],
      profession: [null, Validators.required],
      selectedTrades: [[], Validators.required]
    });
    this.fourthForm = this.fb.group({
      urgent: [null],
      maxPrice: [null]
    });
  }

  ngOnInit(): void {
    this.fiveDaysAhead = new Date();
    this.fiveDaysAhead.setDate(this.fiveDaysAhead.getDate() + 7);
    this.fiveDaysAhead.setMonth(this.fiveDaysAhead.getMonth());
    this.fiveDaysAhead.setFullYear(this.fiveDaysAhead.getFullYear());
    this.locationService.getLocation().subscribe(
      result => {
        this.currentLocation = result.responseObject;
        this.secondForm.controls.address.setValue(this.currentLocation.name);
      }, error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );

  }

  dragEnd(locationModel: LocationModel): void {
    this.currentLocation = locationModel;
    this.secondForm.controls.address.setValue(locationModel.name);
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
      },
      error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  clicked(type: string): void {
    if (type === 'firstFormNext') {
      this.secondFormClicked = true;
    } else if (type === 'secondFormBack') {
      this.secondFormClicked = false;
    } else if (type === 'secondFormNext') {
      this.secondFormClicked = false;
      this.getCategories();
    } else if (type === 'thirdFormBack') {
      this.secondFormClicked = true;
    } else if (type === 'additionalData') {
      this.fillOutAdditionalData = true;
    } else if (type === 'additionalDataGiveUp') {
      this.fillOutAdditionalData = false;
    }
  }

  createJobAd(): void {
    this.spinnerService.show();
    let jobAd: JobAdModel;
    if (!this.fourthForm.value.maxPrice && !this.fourthForm.value.urgent) {
      jobAd = new JobAdModel(
        0,
        this.firstForm.value.title,
        this.firstForm.value.description,
        this.currentLocation,
        null,
        this.secondForm.value.date,
        this.thirdForm.value.selectedTrades,
        this.pictures
      );
    } else {
      jobAd = new JobAdModel(
        0,
        this.firstForm.value.title,
        this.firstForm.value.description,
        this.currentLocation,
        new AdditionalJobAdInfoModel(0, this.fourthForm.value.urgent, this.fourthForm.value.maxPrice),
        this.secondForm.value.date,
        this.thirdForm.value.selectedTrades,
        this.pictures
      );
    }


    this.jobAdService.createJobAd(jobAd).subscribe(
      result => {
        this.spinnerService.hide();
        this.snackBar.open(result.message, 'Ok', {duration: 5000});
        this.router.navigate(['/']);
      },
      error => {
        this.spinnerService.hide();
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );

  }

  changedMatStep($event: StepperSelectionEvent): void {
    this.secondFormClicked = $event.selectedIndex === 1;
  }

  onFileChanged = (event: any) => {
    const file = event.dataTransfer ? event.dataTransfer.files[0] : event.target.files[0];
    const pattern = /image-*/;
    const reader = new FileReader();
    if (!file) {
      return;
    }
    if (!file.type.match(pattern)) {
      alert('invalid format');
      return;
    }
    reader.onload = this.handleReaderLoaded.bind(this);
    reader.readAsDataURL(file);
    this.fileInput.nativeElement.value = '';
  }
  handleReaderLoaded = (event: any) => {
    const reader = event.target;
    const picture = reader.result.replace(/(\r\n\t|\n|\r\t)/gm, '');
    this.pictures.push(picture);
  }

  clickedPicture(picture: any): void {
    this.pictures.forEach((item, index) => {
      if (item === picture) {
        this.pictures.splice(index, 1);
        return;
      }
    });
  }
}
