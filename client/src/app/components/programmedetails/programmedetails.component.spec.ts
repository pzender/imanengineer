import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgrammedetailsComponent } from './programmedetails.component';

describe('ProgrammedetailsComponent', () => {
  let component: ProgrammedetailsComponent;
  let fixture: ComponentFixture<ProgrammedetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProgrammedetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgrammedetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
