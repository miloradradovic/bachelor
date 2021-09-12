import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';
import {MatSnackBar} from '@angular/material/snack-bar';
import {TradeService} from '../../../services/trade.service';
import {HandymanService} from '../../../services/handyman.service';
import {MatSelectChange} from '@angular/material/select';
import {SearchParams} from '../../../model/search-params';
import {MatDialog} from '@angular/material/dialog';
import {DetailedHandymanDialogComponent} from '../../dialogs/detailed-handyman-dialog/detailed-handyman-dialog.component';
import {FlatTreeControl} from '@angular/cdk/tree';
import {MatTreeFlatDataSource, MatTreeFlattener} from '@angular/material/tree';
import {CategoryService} from '../../../services/category.service';


interface Node {
  name: string;
  children?: Node[];
}

/** Flat node with expandable and level information */
interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
}


@Component({
  selector: 'app-handymen-dashboard',
  templateUrl: './handymen-dashboard.component.html',
  styleUrls: ['./handymen-dashboard.component.css']
})
export class HandymenDashboardComponent implements OnInit {

  // tslint:disable-next-line:variable-name
  private _transformer = (node: Node, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.name,
      level,
    };
  }

  treeControl = new FlatTreeControl<ExampleFlatNode>(
    node => node.level, node => node.expandable);

  treeFlattener = new MatTreeFlattener(
    this._transformer, node => node.level, node => node.expandable, node => node.children);

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  form: FormGroup;
  private fb: FormBuilder;
  trades = [];
  handymen: [] = [];
  ogHandymen: [] = [];
  handymanProfession = '';

  constructor(
    fb: FormBuilder,
    public router: Router,
    private spinnerService: NgxSpinnerService,
    private snackBar: MatSnackBar,
    private tradeService: TradeService,
    private handymanService: HandymanService,
    public dialog: MatDialog,
    private categoryService: CategoryService
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
  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;

  ngOnInit(): void {
    this.getCategoriesWithProfessions();
  }

  getCategoriesWithProfessions(): void {
    this.categoryService.getCategoriesWithProfessions().subscribe(
      result => {
        const treeData = [];
        result.responseObject.forEach((item, index) => {
          const node = {name: item.name, children: []};
          // tslint:disable-next-line:no-shadowed-variable
          item.professions.forEach((item, index) => {
            node.children.push({name: item, children: [], selected: false});
          });
          treeData.push(node);
        });
        this.handymanProfession = treeData[0].children[0].name;
        this.getHandymenByProfession(this.handymanProfession);
        this.getTradesByProfession(this.handymanProfession);
        this.dataSource.data = treeData;
      }, error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  selectionChange($event: MatSelectChange): void {
    this.filter();
  }

  onInputChange(object): void {
    this.filter();
  }

  filter(): void {
    const searchParams: SearchParams = new SearchParams(
      this.form.value.firstName,
      this.form.value.lastName,
      this.form.value.selectedTrades,
      this.form.value.avgRatingFrom,
      this.form.value.avgRatingTo,
      this.form.value.address,
      this.ogHandymen
    );
    if (searchParams.firstName === '' && searchParams.address === '' && searchParams.trades.length === 0 &&
      searchParams.avgRatingTo === 5 && searchParams.avgRatingFrom === 0 && searchParams.lastName === '') {
      this.getHandymenByProfession(this.handymanProfession);
      return;
    }
    this.handymanService.filter(searchParams).subscribe(
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

  getTradesByProfession(profession: string): void {
    this.handymanProfession = profession;
    this.tradeService.getTradesByProfessionName(profession).subscribe(
      result => {
        this.trades = result.responseObject;
        this.getHandymenByProfession(profession);
      },
      error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }

  private getHandymenByProfession(profession): void {
    this.form.controls.firstName.setValue('');
    this.form.controls.lastName.setValue('');
    this.form.controls.selectedTrades.setValue([]);
    this.form.controls.avgRatingFrom.setValue(0);
    this.form.controls.avgRatingTo.setValue(5);
    this.form.controls.address.setValue('');

    this.handymanService.getHandymenByProfession(profession).subscribe(
      result => {
        this.ogHandymen = result.responseObject;
        this.handymen = result.responseObject;
      }, error => {
        this.snackBar.open(error.error.message, 'Ok', {duration: 3000});
      }
    );
  }
}
