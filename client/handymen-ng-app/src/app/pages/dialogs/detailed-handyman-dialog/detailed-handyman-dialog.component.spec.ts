import {ComponentFixture, TestBed} from '@angular/core/testing';

import {DetailedHandymanDialogComponent} from './detailed-handyman-dialog.component';

describe('DetailedHandymanDialogComponent', () => {
  let component: DetailedHandymanDialogComponent;
  let fixture: ComponentFixture<DetailedHandymanDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DetailedHandymanDialogComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailedHandymanDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
