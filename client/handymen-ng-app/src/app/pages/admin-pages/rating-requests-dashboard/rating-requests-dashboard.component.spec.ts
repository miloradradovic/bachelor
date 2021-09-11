import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RatingRequestsDashboardComponent } from './rating-requests-dashboard.component';

describe('RatingRequestsDashboardComponent', () => {
  let component: RatingRequestsDashboardComponent;
  let fixture: ComponentFixture<RatingRequestsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RatingRequestsDashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RatingRequestsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
