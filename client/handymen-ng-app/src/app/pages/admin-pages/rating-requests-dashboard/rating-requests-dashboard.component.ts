import {Component, OnInit} from '@angular/core';
import {RatingService} from '../../../services/rating.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {NgxSpinnerService} from 'ngx-spinner';

@Component({
  selector: 'app-rating-requests-dashboard',
  templateUrl: './rating-requests-dashboard.component.html',
  styleUrls: ['./rating-requests-dashboard.component.css']
})
export class RatingRequestsDashboardComponent implements OnInit {

  ratings = [];

  constructor(
    private ratingService: RatingService,
    private snackBar: MatSnackBar,
    private spinnerService: NgxSpinnerService
  ) {
  }

  ngOnInit(): void {
    this.getUnverifiedRatings();
  }

  getUnverifiedRatings(): void {
    this.ratingService.getUnverifiedRatings().subscribe(result => {
      this.ratings = result.responseObject;
    }, error => {
      this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
    });
  }

  decline($event: number): void {
    this.spinnerService.show();
    this.ratingService.deleteRating($event).subscribe(result => {
      this.spinnerService.hide();
      this.snackBar.open(result.message, 'Ok', {duration: 3000});
      this.getUnverifiedRatings();
    }, error => {
      this.spinnerService.hide();
      this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
    });
  }

  verify($event: number): void {
    this.spinnerService.show();
    this.ratingService.verifyRating($event).subscribe(result => {
      this.spinnerService.hide();
      this.snackBar.open(result.message, 'Ok', {duration: 3000});
      this.getUnverifiedRatings();
    }, error => {
      this.spinnerService.hide();
      this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
    });
  }
}
