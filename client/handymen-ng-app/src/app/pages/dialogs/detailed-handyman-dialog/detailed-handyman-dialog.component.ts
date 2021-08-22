import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from '@angular/material/dialog';
import {HandymanService} from '../../../services/handyman.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {RegisteringDecideComponent} from '../registering-decide/registering-decide.component';

@Component({
  selector: 'app-detailed-handyman-dialog',
  templateUrl: './detailed-handyman-dialog.component.html',
  styleUrls: ['./detailed-handyman-dialog.component.css']
})
export class DetailedHandymanDialogComponent implements OnInit {

  handymanId: number = 0;
  handymanName: string = '';
  handymanEmail: string = '';
  handymanAddress: string = '';
  handymanAvgRating: number = 0;
  handymanTrades: string = '';
  commentsEmpty: boolean = true;
  commentIndex: number = 0;
  commentDescription: string = '';
  commentRate: number = 0;
  commentUser: string = '';
  commentPublishedDate: string = '';
  comments: [] = [];
  latitude: number = 0;
  longitude: number = 0;
  radius: number = 0;


  constructor(public dialogRef: MatDialogRef<DetailedHandymanDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: number,
              private handymanService: HandymanService,
              private snackBar: MatSnackBar,
              public dialog: MatDialog) { }

  ngOnInit(): void {
    this.getHandymanData();
  }

  getHandymanData() {
    this.handymanService.getHandymanById(this.data).subscribe(
      result => {
        let resultObj = result.responseObject;
        this.latitude = resultObj.address.latitude;
        this.longitude = resultObj.address.longitude;
        this.handymanAddress = resultObj.address.name;
        this.handymanAvgRating = resultObj.avgRating;
        this.handymanEmail = resultObj.email;
        this.handymanId = resultObj.id;
        this.handymanName = resultObj.name;
        this.radius = resultObj.radius;
        this.comments = resultObj.ratings;
        if (this.comments.length == 0) {
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
        })
      }, error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    )
  }

  commentNavigateBefore() {
    this.commentIndex = this.commentIndex - 1;
    this.fillCommentData();
  }

  commentNavigateNext() {
    this.commentIndex = this.commentIndex + 1;
    this.fillCommentData();
  }

  fillCommentData() {
    // @ts-ignore
    this.commentDescription = this.comments[this.commentIndex].description;
    // @ts-ignore
    this.commentPublishedDate = new Date(this.comments[this.commentIndex].publishedDate).toLocaleString();
    // @ts-ignore
    this.commentRate = this.comments[this.commentIndex].rate;
    // @ts-ignore
    this.commentUser = this.comments[this.commentIndex].userEmail;
  }

  offerJob() {
    /*
    const dialogRef = this.dialog.open(RegisteringDecideComponent, {
      width: '30%'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result === 'user') {
        //this.router.navigate(['/user/register']);
      } else if (result === 'handyman') {
        //this.router.navigate(['/handyman/register']);
      }
    });
     */
  }

  cancel() {
    this.dialogRef.close();
  }
}
