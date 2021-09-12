import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {JobAdService} from '../../../services/job-ad.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-select-job-ad-dialog',
  templateUrl: './select-job-ad-dialog.component.html',
  styleUrls: ['./select-job-ad-dialog.component.css']
})
export class SelectJobAdDialogComponent implements OnInit {

  form: FormGroup;
  jobAds = [];
  private fb: FormBuilder;

  constructor(
    fb: FormBuilder,
    private jobAdService: JobAdService,
    public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<SelectJobAdDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { handyTrades: [] }
  ) {
    this.fb = fb;
    this.form = this.fb.group({
      selectedJobAd: [null, [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.getJobAds();
  }

  getJobAds(): void {
    this.jobAdService.getJobAdsWithNoOfferByUser(this.data).subscribe(
      result => {
        this.jobAds = result.responseObject;
      }, error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  cancel(): void {
    this.dialogRef.close();
  }

  finish(): void {
    this.dialogRef.close(this.form.value.selectedJobAd);
  }
}
