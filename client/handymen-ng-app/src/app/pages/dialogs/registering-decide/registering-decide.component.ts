import {Component, OnInit} from '@angular/core';
import {MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-registering-decide',
  templateUrl: './registering-decide.component.html',
  styleUrls: ['./registering-decide.component.css']
})
export class RegisteringDecideComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<RegisteringDecideComponent>) {
  }

  ngOnInit(): void {
  }

  clickedHandyman(): void {
    this.dialogRef.close('handyman');
  }

  clickedUser(): void {
    this.dialogRef.close('user');
  }
}
