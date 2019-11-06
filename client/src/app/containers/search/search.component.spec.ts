import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchComponent } from './search.component';
import { FilterSidebarComponent } from 'src/app/shared/components/filter-sidebar/filter-sidebar.component';
import { OfferPickerComponent } from 'src/app/shared/components/offer-picker/offer-picker.component';
import { ListingComponent } from 'src/app/shared/components/listing/listing.component';
import { ListingElementComponent } from 'src/app/shared/components/listing-element/listing-element.component';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ServiceWorkerModule } from '@angular/service-worker';

describe('SearchComponent', () => {
  let component: SearchComponent;
  let fixture: ComponentFixture<SearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        SearchComponent, 
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
    fixture = TestBed.createComponent(SearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
