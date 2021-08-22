import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';
import {MatSnackBar} from '@angular/material/snack-bar';
import {TradeService} from '../../../services/trade.service';
import {HandymanService} from '../../../services/handyman.service';
import {MatSelectChange} from '@angular/material/select';
import {SearchParams} from '../../../model/search-params';
import {RegisteringDecideComponent} from '../../dialogs/registering-decide/registering-decide.component';
import {MatDialog} from '@angular/material/dialog';
import {DetailedHandymanDialogComponent} from '../../dialogs/detailed-handyman-dialog/detailed-handyman-dialog.component';

@Component({
  selector: 'app-handymen-dashboard',
  templateUrl: './handymen-dashboard.component.html',
  styleUrls: ['./handymen-dashboard.component.css']
})
export class HandymenDashboardComponent implements OnInit {

  form: FormGroup;
  private fb: FormBuilder;
  trades = []
  //selectedTrades = new FormControl();
  handymen = []

  constructor(
    fb: FormBuilder,
    public router: Router,
    private spinnerService: NgxSpinnerService,
    private snackBar: MatSnackBar,
    private tradeService: TradeService,
    private handymanService: HandymanService,
    public dialog: MatDialog
  ) {
    this.fb = fb;
    this.form = this.fb.group({
      firstName: [null],
      lastName: [null],
      avgRatingFrom: [0],
      avgRatingTo: [5],
      selectedTrades: [[]],
      address: [null]
    });
  }

  ngOnInit(): void {
    this.getHandymen();
    this.getTrades();
  }

  getTrades() {
    this.tradeService.getTrades().subscribe(
      result => {
        this.trades = result.responseObject;
      },
      error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    )
  }

  getHandymen() {
    this.handymanService.getAllHandymen().subscribe(
      result => {
        console.log(result);
        this.handymen = result.responseObject;
      },
      error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    )
  }

  selectionChange($event: MatSelectChange) {
    this.search();
  }

  onInputChange(object: Object) {
    this.search();
  }

  search() {
    let searchParams: SearchParams = new SearchParams(
      this.form.value.firstName,
      this.form.value.lastName,
      this.form.value.selectedTrades,
      this.form.value.avgRatingFrom,
      this.form.value.avgRatingTo,
      this.form.value.address
    )
    this.handymanService.search(searchParams).subscribe(
      result => {
        this.handymen = result.responseObject;
      }, error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    )
  }

  clickedRow($event: number) {
    const dialogRef = this.dialog.open(DetailedHandymanDialogComponent, {
      width: '60%',
      height: '80%',
      data: $event
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result === 'user') {
        this.router.navigate(['/user/register']);
      } else if (result === 'handyman') {
        this.router.navigate(['/handyman/register']);
      }
    });
  }
}
