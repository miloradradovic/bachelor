import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';
import {MatSnackBar} from '@angular/material/snack-bar';
import {InterestService} from '../../../services/interest.service';
import {JobService} from '../../../services/job.service';

@Component({
  selector: 'app-interests-dashboard',
  templateUrl: './interests-dashboard.component.html',
  styleUrls: ['./interests-dashboard.component.css']
})
export class InterestsDashboardComponent implements OnInit {

  interests = [];

  constructor(
    public router: Router,
    private spinnerService: NgxSpinnerService,
    private snackBar: MatSnackBar,
    private jobService: JobService,
    private interestService: InterestService
  ) { }

  ngOnInit(): void {
    this.getInterestsByUser();
  }

  getInterestsByUser() {
    this.interestService.getInterestsForCurrentUser().subscribe(
      result => {
        this.interests = result.responseObject;
      }, error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    )
  }

  makeJobDeal($event: number) {
    this.spinnerService.show();
    this.jobService.createJob($event).subscribe(
      result => {
        this.spinnerService.hide();
        this.snackBar.open(result.message, 'Ok', {duration: 3000});
        this.router.navigate(['/']);
      }, error => {
        this.spinnerService.hide();
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    )
  }

  handymanDetails($event: number) {

  }
}
