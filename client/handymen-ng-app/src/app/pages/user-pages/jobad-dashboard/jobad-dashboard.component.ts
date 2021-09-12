import {Component, OnInit} from '@angular/core';
import {JobAdService} from '../../../services/job-ad.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {PicturesDialogComponent} from '../../dialogs/pictures-dialog/pictures-dialog.component';
import {MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'app-jobad-dashboard',
  templateUrl: './jobad-dashboard.component.html',
  styleUrls: ['./jobad-dashboard.component.css']
})
export class JobadDashboardComponent implements OnInit {

  jobAds = [];

  constructor(
    private jobAdService: JobAdService,
    private snackBar: MatSnackBar,
    public dialog: MatDialog
  ) {
  }

  ngOnInit(): void {
    this.getJobAdsByUser();
  }

  getJobAdsByUser(): void {
    this.jobAdService.getJobAdsByUser().subscribe(
      result => {
        this.jobAds = result.responseObject;
      }, error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  viewPics(pictures): void {
    const dialogRef = this.dialog.open(PicturesDialogComponent, {
      width: '80%',
      data: {pictures}
    });
  }

}
