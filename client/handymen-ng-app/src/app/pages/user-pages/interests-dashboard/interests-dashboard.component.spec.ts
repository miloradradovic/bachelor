import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InterestsDashboardComponent } from './interests-dashboard.component';

describe('InterestsDashboardComponent', () => {
  let component: InterestsDashboardComponent;
  let fixture: ComponentFixture<InterestsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InterestsDashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InterestsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
