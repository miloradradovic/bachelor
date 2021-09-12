import {ComponentFixture, TestBed} from '@angular/core/testing';

import {JobAdDashboardComponent} from './job-ad-dashboard.component';

describe('JobAdDashboardComponent', () => {
  let component: JobAdDashboardComponent;
  let fixture: ComponentFixture<JobAdDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [JobAdDashboardComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JobAdDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
