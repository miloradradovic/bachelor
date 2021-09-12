import {Component, EventEmitter, Input, OnChanges, Output, SimpleChanges} from '@angular/core';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnChanges{

  @Input() dataSource = [];
  @Input() columnsToDisplay = [];
  @Input() columnsToIterate = [];
  @Output() MakeInterest = new EventEmitter<number>();
  @Output() Click = new EventEmitter<number>();
  @Output() DoubleClick = new EventEmitter<number>();
  @Output() HandymanDetails = new EventEmitter<number>();
  @Output() MakeJobDeal = new EventEmitter<number>();
  @Output() FinishJob = new EventEmitter<number>();
  @Output() RateJob = new EventEmitter<number>();
  @Output() Verify = new EventEmitter<number>();
  @Output() Decline = new EventEmitter<number>();
  @Output() ViewPics = new EventEmitter<any>();

  constructor() {

  }

  ngOnChanges(changes: SimpleChanges): void {
    for (const propName in changes) {
      if (changes.hasOwnProperty(propName)) {
        let vary = this.get(propName);
        vary = changes[propName].currentValue;
      }
    }
  }

  makeInterest(id: number): void {
    this.MakeInterest.emit(id);
  }

  clicked(id: number): void {
    this.Click.emit(id);
  }

  doubleClicked(id: number): void {
    this.DoubleClick.emit(id);
  }

  get(element: string): string[] {
    switch (element) {
      case 'dataSource':
        return this.dataSource;
      case 'columnsToDisplay':
        return this.columnsToDisplay;
      default:
        return this.columnsToIterate;
    }
  }

  handymanDetails(id): void {
    this.HandymanDetails.emit(id);
  }

  makeJobDeal(id): void {
    this.MakeJobDeal.emit(id);
  }

  finishJob(id): void {
    this.FinishJob.emit(id);
  }

  rateJob(id): void {
    this.RateJob.emit(id);
  }

  verify(id): void {
    this.Verify.emit(id);
  }

  decline(id): void {
    this.Decline.emit(id);
  }

  viewPics(pics): void {
    this.ViewPics.emit(pics);
  }
}
