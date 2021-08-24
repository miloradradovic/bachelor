import { Component, OnInit } from '@angular/core';
import {JobAdService} from '../../../services/job-ad.service';
import {MatSnackBar} from '@angular/material/snack-bar';

@Component({
  selector: 'app-jobad-dashboard',
  templateUrl: './jobad-dashboard.component.html',
  styleUrls: ['./jobad-dashboard.component.css']
})
export class JobadDashboardComponent implements OnInit {

  jobAds = [];

  constructor(
    private jobAdService: JobAdService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.getJobAdsByUser();
  }

  getJobAdsByUser() {
    this.jobAdService.getJobAdsByUser().subscribe(
      result => {
        this.jobAds = result.responseObject;
      }, error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    )
  }

}
