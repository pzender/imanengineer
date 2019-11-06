import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChannelListingComponent } from './channel-listing.component';
import { FilterSidebarComponent } from 'src/app/shared/components/filter-sidebar/filter-sidebar.component';
import { OfferPickerComponent } from 'src/app/shared/components/offer-picker/offer-picker.component';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ListingComponent } from 'src/app/shared/components/listing/listing.component';
import { ListingElementComponent } from 'src/app/shared/components/listing-element/listing-element.component';
import { ServiceWorkerModule } from '@angular/service-worker';

describe('ChannelListingComponent', () => {
  let component: ChannelListingComponent;
  let fixture: ComponentFixture<ChannelListingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        ChannelListingComponent, 
        FilterSidebarComponent, 
        OfferPickerComponent, 
        ListingComponent, 
        ListingElementComponent 
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
    fixture = TestBed.createComponent(ChannelListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
