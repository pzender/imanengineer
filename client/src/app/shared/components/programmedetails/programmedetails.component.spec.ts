import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgrammedetailsComponent } from './programmedetails.component';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ServiceWorkerModule } from '@angular/service-worker';

describe('ProgrammedetailsComponent', () => {
  let component: ProgrammedetailsComponent;
  let fixture: ComponentFixture<ProgrammedetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProgrammedetailsComponent ],
      imports: [
        FormsModule, 
        HttpClientTestingModule, 
        RouterTestingModule,
        ServiceWorkerModule.register('', {enabled: false})
      ]

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
