import {ComponentFixture, TestBed} from '@angular/core/testing';

import {NavbarNonSignedInComponent} from './navbar-non-signed-in.component';

describe('NavbarNonSignedInComponent', () => {
  let component: NavbarNonSignedInComponent;
  let fixture: ComponentFixture<NavbarNonSignedInComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NavbarNonSignedInComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NavbarNonSignedInComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
