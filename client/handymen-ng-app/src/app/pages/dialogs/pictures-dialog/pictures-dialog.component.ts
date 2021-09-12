import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {MatSnackBar} from '@angular/material/snack-bar';

@Component({
  selector: 'app-pictures-dialog',
  templateUrl: './pictures-dialog.component.html',
  styleUrls: ['./pictures-dialog.component.css']
})
export class PicturesDialogComponent implements OnInit {

  pictures = [];

  constructor(
    public dialogRef: MatDialogRef<PicturesDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { pictures: [] },
    private snackBar: MatSnackBar
  ) {
  }

  ngOnInit(): void {
    this.pictures = this.data.pictures;
    if (this.pictures.length === 0) {
      this.snackBar.open('Nema slika!', 'Ok', {duration: 3000});
      this.dialogRef.close();
    }
  }

}
