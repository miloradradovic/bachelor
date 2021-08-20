import {Component, Input, Output, EventEmitter, SimpleChanges} from '@angular/core';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent {

  @Input() dataSource = [];
  @Input() columnsToDisplay = [];
  @Input() columnsToIterate = [];
  @Output() MakeInterest = new EventEmitter<number>();
  @Output() Click = new EventEmitter<number>();
  @Output() DoubleClick = new EventEmitter<number>();
  constructor() {

  }

  ngOnChanges(changes: SimpleChanges): void {
    for (const propName in changes) {
      if (changes.hasOwnProperty(propName)){
        let vary = this.get(propName);
        vary = changes[propName].currentValue;
      }
    }
  }
  makeInterest(id: number){
    this.MakeInterest.emit(id);
  }
  clicked(id: number){
    this.Click.emit(id);
  }
  doubleClicked(id: number){
    this.DoubleClick.emit(id);
  }
  get(element: string): string[]{
    switch (element) {
      case 'dataSource':
        return this.dataSource;
      case 'columnsToDisplay':
        return this.columnsToDisplay;
      default:
        return this.columnsToIterate;
    }
  }
}
