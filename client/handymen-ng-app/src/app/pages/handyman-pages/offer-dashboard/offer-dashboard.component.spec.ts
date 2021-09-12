import {ComponentFixture, TestBed} from '@angular/core/testing';

import {OfferDashboardComponent} from './offer-dashboard.component';

describe('OfferDashboardComponent', () => {
  let component: OfferDashboardComponent;
  let fixture: ComponentFixture<OfferDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OfferDashboardComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OfferDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
