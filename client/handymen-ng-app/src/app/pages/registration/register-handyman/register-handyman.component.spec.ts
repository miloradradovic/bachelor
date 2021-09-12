import {ComponentFixture, TestBed} from '@angular/core/testing';

import {RegisterHandymanComponent} from './register-handyman.component';

describe('RegisterHandymanComponent', () => {
  let component: RegisterHandymanComponent;
  let fixture: ComponentFixture<RegisterHandymanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RegisterHandymanComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterHandymanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
