import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-create-interest-dialog',
  templateUrl: './create-interest-dialog.component.html',
  styleUrls: ['./create-interest-dialog.component.css']
})
export class CreateInterestDialogComponent implements OnInit {

  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<CreateInterestDialogComponent>
  ) {
    this.fb = fb;
    this.form = this.fb.group({
      price: [null, [Validators.required]],
      days: [null, Validators.required]
    });
  }

  ngOnInit(): void {
  }

  submit() {
    this.dialogRef.close({price: this.form.value.price, days: this.form.value.days});
  }

  cancel() {
    this.dialogRef.close();
  }
}
