import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';
import {JobService} from '../../../services/job.service';
import {MatSnackBar} from '@angular/material/snack-bar';

@Component({
  selector: 'app-job-dashboard',
  templateUrl: './job-dashboard.component.html',
  styleUrls: ['./job-dashboard.component.css']
})
export class JobDashboardComponent implements OnInit {

  jobs = [];

  constructor(
    public router: Router,
    private spinnerService: NgxSpinnerService,
    private jobService: JobService,
    private snackBar: MatSnackBar
  ) {
  }

  ngOnInit(): void {
    this.getJobsForHandyman();
  }

  getJobsForHandyman(): void {
    this.jobService.getJobsForHandyman().subscribe(
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
        this.getJobsForHandyman();
      }, error => {
        this.spinnerService.hide();
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

}
