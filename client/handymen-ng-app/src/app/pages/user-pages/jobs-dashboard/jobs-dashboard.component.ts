import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';
import {JobService} from '../../../services/job.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {MatDialog} from '@angular/material/dialog';
import {RatingFormComponent} from '../../dialogs/rating-form/rating-form.component';
import {RatingModel} from '../../../model/rating.model';
import {RatingService} from '../../../services/rating.service';

@Component({
  selector: 'app-jobs-dashboard',
  templateUrl: './jobs-dashboard.component.html',
  styleUrls: ['./jobs-dashboard.component.css']
})
export class JobsDashboardComponent implements OnInit {

  jobs = [];

  constructor(
    public router: Router,
    private spinnerService: NgxSpinnerService,
    private jobService: JobService,
    private snackBar: MatSnackBar,
    public dialog: MatDialog,
    private ratingService: RatingService
  ) {
  }

  ngOnInit(): void {
    this.getJobsForUser();
  }

  getJobsForUser(): void {
    this.jobService.getJobsForUser().subscribe(
      result => {
        this.jobs = result.responseObject;
      }, error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  finishJob($event: number): void {
    this.spinnerService.show();
    this.jobService.finishJob($event).subscribe(
      result => {
        this.spinnerService.hide();
        this.snackBar.open(result.message, 'Ok', {duration: 3000});
        this.getJobsForUser();
      }, error => {
        this.spinnerService.hide();
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  rateJob($event: number): void {
    const dialogRef = this.dialog.open(RatingFormComponent, {
      width: '25%'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.spinnerService.show();
        const ratingModel: RatingModel = new RatingModel(0, result.rating, result.comment, $event);

        this.ratingService.createRating(ratingModel).subscribe(
          // tslint:disable-next-line:no-shadowed-variable
          result => {
            this.spinnerService.hide();
            this.snackBar.open(result.message, 'Ok', {duration: 3000});
            this.getJobsForUser();
          }, error => {
            this.spinnerService.hide();
            this.snackBar.open(error.message.message, 'Ok', {duration: 3000});

          }
        );

      }
    });
  }
}
