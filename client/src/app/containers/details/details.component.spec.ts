import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailsComponent } from './details.component';
import { FilterSidebarComponent } from 'src/app/shared/components/filter-sidebar/filter-sidebar.component';
import { OfferPickerComponent } from 'src/app/shared/components/offer-picker/offer-picker.component';
import { ListingComponent } from 'src/app/shared/components/listing/listing.component';
import { ListingElementComponent } from 'src/app/shared/components/listing-element/listing-element.component';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ServiceWorkerModule } from '@angular/service-worker';
import { ProgrammedetailsComponent } from 'src/app/shared/components/programmedetails/programmedetails.component';

describe('DetailsComponent', () => {
  let component: DetailsComponent;
  let fixture: ComponentFixture<DetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        DetailsComponent, 
        FilterSidebarComponent, 
        OfferPickerComponent, 
        ListingComponent, 
        ListingElementComponent,
        ProgrammedetailsComponent
      ],
      imports: [
        RouterTestingModule, 
        FormsModule, 
        HttpClientTestingModule, 
        ServiceWorkerModule.register('', {enabled: false})
    ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
