import {ComponentFixture, TestBed} from '@angular/core/testing';

import {CreateJobadFormComponent} from './create-jobad-form.component';

describe('CreateJobadFormComponent', () => {
  let component: CreateJobadFormComponent;
  let fixture: ComponentFixture<CreateJobadFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateJobadFormComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateJobadFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
