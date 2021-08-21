import { Component, OnInit } from '@angular/core';
import {MatDialogRef} from '@angular/material/dialog';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-rating-form',
  templateUrl: './rating-form.component.html',
  styleUrls: ['./rating-form.component.css']
})
export class RatingFormComponent implements OnInit {

  form: FormGroup;
  private fb: FormBuilder;

  constructor(public dialogRef: MatDialogRef<RatingFormComponent>,
              fb: FormBuilder) {
    this.fb = fb;
    this.form = this.fb.group({
      commentInput: ['', [Validators.required]],
      pickedRating: [5, [Validators.required]]
    });
  }

  ngOnInit(): void {
  }

  submit() {
    this.dialogRef.close({comment: this.form.value.commentInput, rating: this.form.value.pickedRating});
  }

  cancel() {
    this.dialogRef.close();
  }
}
