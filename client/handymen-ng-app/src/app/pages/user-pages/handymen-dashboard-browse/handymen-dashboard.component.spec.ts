import {ComponentFixture, TestBed} from '@angular/core/testing';

import {HandymenDashboardComponent} from './handymen-dashboard.component';

describe('HandymenDashboardComponent', () => {
  let component: HandymenDashboardComponent;
  let fixture: ComponentFixture<HandymenDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HandymenDashboardComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HandymenDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
