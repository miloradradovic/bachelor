import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-decline-reason-dialog',
  templateUrl: './decline-reason-dialog.component.html',
  styleUrls: ['./decline-reason-dialog.component.css']
})
export class DeclineReasonDialogComponent implements OnInit {

  form: FormGroup;
  private fb: FormBuilder;

  constructor(
    fb: FormBuilder,
    public dialogRef: MatDialogRef<DeclineReasonDialogComponent>
  ) {
    this.fb = fb;
    this.form = this.fb.group({
      reason: [null, [Validators.required]]
    });
  }

  ngOnInit(): void {
  }

  cancel() {
    this.dialogRef.close();
  }

  submit() {
    this.dialogRef.close(this.form.value.reason);
  }
}
