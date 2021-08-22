import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectJobAdDialogComponent } from './select-job-ad-dialog.component';

describe('SelectJobAdDialogComponent', () => {
  let component: SelectJobAdDialogComponent;
  let fixture: ComponentFixture<SelectJobAdDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelectJobAdDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectJobAdDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
