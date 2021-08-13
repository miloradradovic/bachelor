import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisteringDecideComponent } from './registering-decide.component';

describe('RegisteringDecideComponent', () => {
  let component: RegisteringDecideComponent;
  let fixture: ComponentFixture<RegisteringDecideComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegisteringDecideComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisteringDecideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
