import {ComponentFixture, TestBed} from '@angular/core/testing';

import {RegistrationRequestsDashboardComponent} from './registration-requests-dashboard.component';

describe('RegistrationRequestsDashboardComponent', () => {
  let component: RegistrationRequestsDashboardComponent;
  let fixture: ComponentFixture<RegistrationRequestsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RegistrationRequestsDashboardComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrationRequestsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
