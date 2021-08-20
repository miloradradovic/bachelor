import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateInterestDialogComponent } from './create-interest-dialog.component';

describe('CreateInterestDialogComponent', () => {
  let component: CreateInterestDialogComponent;
  let fixture: ComponentFixture<CreateInterestDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateInterestDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateInterestDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
