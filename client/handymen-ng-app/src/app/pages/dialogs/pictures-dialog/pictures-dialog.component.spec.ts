import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PicturesDialogComponent } from './pictures-dialog.component';

describe('PicturesDialogComponent', () => {
  let component: PicturesDialogComponent;
  let fixture: ComponentFixture<PicturesDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PicturesDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PicturesDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
