import {ComponentFixture, TestBed} from '@angular/core/testing';

import {HandymanProfileComponent} from './handyman-profile.component';

describe('HandymanProfileComponent', () => {
  let component: HandymanProfileComponent;
  let fixture: ComponentFixture<HandymanProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HandymanProfileComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HandymanProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
