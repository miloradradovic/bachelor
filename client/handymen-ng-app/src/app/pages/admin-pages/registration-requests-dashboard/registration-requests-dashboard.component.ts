import {Component, OnInit} from '@angular/core';
import {LocationModel} from '../../../model/location.model';
import {HandymanService} from '../../../services/handyman.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {MatDialog} from '@angular/material/dialog';
import {HandymanVerificationModel} from '../../../model/handyman-verification.model';
import {DeclineReasonDialogComponent} from '../../dialogs/decline-reason-dialog/decline-reason-dialog.component';
import {NgxSpinnerService} from 'ngx-spinner';

@Component({
  selector: 'app-registration-requests-dashboard',
  templateUrl: './registration-requests-dashboard.component.html',
  styleUrls: ['./registration-requests-dashboard.component.css']
})
export class RegistrationRequestsDashboardComponent implements OnInit {

  originalResult = [];
  regReqs = [];
  currentLocation: LocationModel = new LocationModel(45.259452102126545, 19.848492145538334, 'Aleksandra Tisme 3, 21101 Novi Sad City, Serbia', 0);


  constructor(
    private handymanService: HandymanService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private spinnerService: NgxSpinnerService
  ) {
  }

  ngOnInit(): void {
    this.getRequests();
  }

  getRequests(): void {
    this.handymanService.getRequests().subscribe(
      result => {
        this.originalResult = result.responseObject;
        const reqs = [];
        this.originalResult.forEach((item, index) => {
          if (index === 0) {
            this.currentLocation = new LocationModel(item.location.latitude, item.location.longitude, item.location.name, item.radius);
          }
          const req = {
            id: item.id,
            firstName: item.firstName,
            lastName: item.lastName,
            address: item.location.name,
            email: item.email
          };
          reqs.push(req);
        });
        this.regReqs = reqs;

      }, error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  onRowClick($event: number): void {
    this.originalResult.forEach((item, index) => {
      if (item.id === $event) {
        this.currentLocation = new LocationModel(item.location.latitude, item.location.longitude, item.location.name, item.radius);
      }
    });
  }

  verify($event: number): void {
    this.spinnerService.show();
    this.handymanService.verify(new HandymanVerificationModel($event, true, '')).subscribe(
      result => {
        this.spinnerService.hide();
        this.snackBar.open(result.message, 'Ok', {duration: 3000});
        this.getRequests();
      }, error => {
        this.spinnerService.hide();
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  decline($event: number): void {
    const dialogRef = this.dialog.open(DeclineReasonDialogComponent, {
      width: '20%',
      height: '30%'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.spinnerService.show();
        this.handymanService.verify(new HandymanVerificationModel($event, false, result)).subscribe(
          // tslint:disable-next-line:no-shadowed-variable
          result => {
            this.spinnerService.hide();
            this.snackBar.open(result.message, 'Ok', {duration: 3000});
            this.getRequests();
          }, error => {
            this.spinnerService.hide();
            this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
          }
        );
      }
    });
  }
}
