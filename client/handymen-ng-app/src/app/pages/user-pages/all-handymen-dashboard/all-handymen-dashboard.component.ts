import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';
import {MatSnackBar} from '@angular/material/snack-bar';
import {TradeService} from '../../../services/trade.service';
import {HandymanService} from '../../../services/handyman.service';
import {MatDialog} from '@angular/material/dialog';
import {MatSelectChange} from '@angular/material/select';
import {SearchParams} from '../../../model/search-params';
import {DetailedHandymanDialogComponent} from '../../dialogs/detailed-handyman-dialog/detailed-handyman-dialog.component';

@Component({
  selector: 'app-all-handymen-dashboard',
  templateUrl: './all-handymen-dashboard.component.html',
  styleUrls: ['./all-handymen-dashboard.component.css']
})
export class AllHandymenDashboardComponent implements OnInit {

  form: FormGroup;
  trades = [];
  handymen = [];
  private fb: FormBuilder;

  constructor(
    fb: FormBuilder,
    public router: Router,
    private spinnerService: NgxSpinnerService,
    private snackBar: MatSnackBar,
    private tradeService: TradeService,
    private handymanService: HandymanService,
    public dialog: MatDialog,
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

  getTrades(): void {
    this.tradeService.getTrades().subscribe(
      result => {
        this.trades = result.responseObject;
      },
      error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  getHandymen(): void {
    this.handymanService.getAllHandymen().subscribe(
      result => {
        this.handymen = result.responseObject;
      },
      error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  selectionChange($event: MatSelectChange): void {
    this.search();
  }

  onInputChange(object): void {
    this.search();
  }

  search(): void {
    const searchParams: SearchParams = new SearchParams(
      this.form.value.firstName,
      this.form.value.lastName,
      this.form.value.selectedTrades,
      this.form.value.avgRatingFrom,
      this.form.value.avgRatingTo,
      this.form.value.address,
      []
    );
    this.handymanService.search(searchParams).subscribe(
      result => {
        this.handymen = result.responseObject;
      }, error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  clickedRow($event: number): void {
    const dialogRef = this.dialog.open(DetailedHandymanDialogComponent, {
      width: '60%',
      height: '80%',
      data: {handymanId: $event, enableOffer: true}
    });
  }

}
