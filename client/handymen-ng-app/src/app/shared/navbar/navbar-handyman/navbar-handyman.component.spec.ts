import {ComponentFixture, TestBed} from '@angular/core/testing';

import {NavbarHandymanComponent} from './navbar-handyman.component';

describe('NavbarHandymanComponent', () => {
  let component: NavbarHandymanComponent;
  let fixture: ComponentFixture<NavbarHandymanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NavbarHandymanComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NavbarHandymanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
