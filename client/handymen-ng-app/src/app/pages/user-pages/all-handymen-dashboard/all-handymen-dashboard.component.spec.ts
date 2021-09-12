import {ComponentFixture, TestBed} from '@angular/core/testing';

import {AllHandymenDashboardComponent} from './all-handymen-dashboard.component';

describe('AllHandymenDashboardComponent', () => {
  let component: AllHandymenDashboardComponent;
  let fixture: ComponentFixture<AllHandymenDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AllHandymenDashboardComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AllHandymenDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
