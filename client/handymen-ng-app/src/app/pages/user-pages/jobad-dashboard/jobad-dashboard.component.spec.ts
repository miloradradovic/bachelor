import {ComponentFixture, TestBed} from '@angular/core/testing';

import {JobadDashboardComponent} from './jobad-dashboard.component';

describe('JobadDashboardComponent', () => {
  let component: JobadDashboardComponent;
  let fixture: ComponentFixture<JobadDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [JobadDashboardComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JobadDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
