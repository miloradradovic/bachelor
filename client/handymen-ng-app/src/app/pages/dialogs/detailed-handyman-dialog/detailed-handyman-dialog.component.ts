import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from '@angular/material/dialog';
import {HandymanService} from '../../../services/handyman.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {SelectJobAdDialogComponent} from '../select-job-ad-dialog/select-job-ad-dialog.component';
import {OfferService} from '../../../services/offer.service';
import {NgxSpinnerService} from 'ngx-spinner';

@Component({
  selector: 'app-detailed-handyman-dialog',
  templateUrl: './detailed-handyman-dialog.component.html',
  styleUrls: ['./detailed-handyman-dialog.component.css']
})
export class DetailedHandymanDialogComponent implements OnInit {

  handymanId = 0;
  handymanName = '';
  handymanEmail = '';
  handymanAddress = '';
  handymanAvgRating = 0;
  handymanTrades = '';
  commentsEmpty = true;
  commentIndex = 0;
  commentDescription = '';
  commentRate = 0;
  commentUser = '';
  commentPublishedDate = '';
  comments: [] = [];
  latitude = 0;
  longitude = 0;
  radius = 0;
  enableOffer = true;
  trades = [];


  constructor(public dialogRef: MatDialogRef<DetailedHandymanDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: { handymanId: number, enableOffer: boolean },
              private handymanService: HandymanService,
              private snackBar: MatSnackBar,
              public dialog: MatDialog,
              private offerService: OfferService,
              private spinnerService: NgxSpinnerService) {
  }

  ngOnInit(): void {
    this.enableOffer = this.data.enableOffer;
    this.getHandymanData();
  }

  getHandymanData(): void {
    this.handymanService.getHandymanById(this.data.handymanId).subscribe(
      result => {
        const resultObj = result.responseObject;
        this.latitude = resultObj.address.latitude;
        this.longitude = resultObj.address.longitude;
        this.handymanAddress = resultObj.address.name;
        this.handymanAvgRating = resultObj.avgRating;
        this.handymanEmail = resultObj.email;
        this.handymanId = resultObj.id;
        this.handymanName = resultObj.name;
        this.radius = resultObj.radius;
        this.comments = resultObj.ratings;
        this.trades = resultObj.trades;
        if (this.comments.length === 0) {
          this.commentsEmpty = true;
        } else {
          this.commentsEmpty = false;
          this.fillCommentData();
        }
        resultObj.trades.forEach((element, index) => {
          if (index === resultObj.trades.length - 1) {
            this.handymanTrades = this.handymanTrades + element;
          } else {
            this.handymanTrades = this.handymanTrades + element + ', ';
          }
        });
      }, error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  commentNavigateBefore(): void {
    this.commentIndex = this.commentIndex - 1;
    this.fillCommentData();
  }

  commentNavigateNext(): void {
    this.commentIndex = this.commentIndex + 1;
    this.fillCommentData();
  }

  fillCommentData(): void {
    // @ts-ignore
    this.commentDescription = this.comments[this.commentIndex].description;
    // @ts-ignore
    this.commentPublishedDate = new Date(this.comments[this.commentIndex].publishedDate).toLocaleString();
    // @ts-ignore
    this.commentRate = this.comments[this.commentIndex].rate;
    // @ts-ignore
    this.commentUser = this.comments[this.commentIndex].userEmail;
  }

  offerJob(): void {
    const dialogRef = this.dialog.open(SelectJobAdDialogComponent, {
      width: '30%',
      data: {handyTrades: this.trades}
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.spinnerService.show();
        this.offerService.makeOffer({jobAd: result, handyMan: this.data.handymanId}).subscribe(
          // tslint:disable-next-line:no-shadowed-variable
          result => {
            this.spinnerService.hide();
            this.snackBar.open(result.message, 'Ok', {duration: 3000});
          }, error => {
            this.spinnerService.hide();
            this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
          }
        );
      }
    });
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
