import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterSidebarComponent } from './filter-sidebar.component';
import { OfferPickerComponent } from '../offer-picker/offer-picker.component';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ServiceWorkerModule } from '@angular/service-worker';

describe('FilterSidebarComponent', () => {
  let component: FilterSidebarComponent;
  let fixture: ComponentFixture<FilterSidebarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        FilterSidebarComponent, 
        OfferPickerComponent, 
      ],
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
    fixture = TestBed.createComponent(FilterSidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
