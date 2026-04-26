import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateAbsence } from './create-absence';

describe('CreateAbsence', () => {
  let component: CreateAbsence;
  let fixture: ComponentFixture<CreateAbsence>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateAbsence]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateAbsence);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
