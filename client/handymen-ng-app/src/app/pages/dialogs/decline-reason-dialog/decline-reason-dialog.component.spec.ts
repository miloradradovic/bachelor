import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeclineReasonDialogComponent } from './decline-reason-dialog.component';

describe('DeclineReasonDialogComponent', () => {
  let component: DeclineReasonDialogComponent;
  let fixture: ComponentFixture<DeclineReasonDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeclineReasonDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeclineReasonDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
