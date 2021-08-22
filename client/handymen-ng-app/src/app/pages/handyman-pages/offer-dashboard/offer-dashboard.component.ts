import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';
import {JobAdService} from '../../../services/job-ad.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {MatDialog} from '@angular/material/dialog';
import {InterestService} from '../../../services/interest.service';
import {CreateInterestDialogComponent} from '../../dialogs/create-interest-dialog/create-interest-dialog.component';
import {InterestModel} from '../../../model/interest.model';
import {OfferService} from '../../../services/offer.service';

@Component({
  selector: 'app-offer-dashboard',
  templateUrl: './offer-dashboard.component.html',
  styleUrls: ['./offer-dashboard.component.css']
})
export class OfferDashboardComponent implements OnInit {

  jobAds = [];

  constructor(
    public router: Router,
    private spinnerService: NgxSpinnerService,
    private jobAdService: JobAdService,
    private snackBar: MatSnackBar,
    public dialog: MatDialog,
    private interestService: InterestService,
    private offerService: OfferService
  ) { }

  ngOnInit(): void {
    this.getOffers();
  }

  getOffers() {
    this.offerService.getOffersByHandyman().subscribe(
      result => {
        this.jobAds = result.responseObject;
      }, error => {
        this.spinnerService.hide();
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    )
  }

  makeInterest($event: number) {

    const dialogRef = this.dialog.open(CreateInterestDialogComponent, {
      width: '30%'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.spinnerService.show();
        let interest: InterestModel = new InterestModel(0, $event, result.days, result.price);
        this.interestService.createInterest(interest).subscribe(
          result => {
            this.spinnerService.hide();
            this.snackBar.open(result.message, 'Ok', {duration: 3000});
            this.getOffers();
          }, error => {
            this.spinnerService.hide();
            this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
          }
        )
      }
    });

  }

}
