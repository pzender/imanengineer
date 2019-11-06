import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListingComponent } from './listing.component';
import { ListingElementComponent } from '../listing-element/listing-element.component';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ServiceWorkerModule } from '@angular/service-worker';

describe('ListingComponent', () => {
  let component: ListingComponent;
  let fixture: ComponentFixture<ListingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListingComponent, ListingElementComponent ],
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
    fixture = TestBed.createComponent(ListingComponent);
    component = fixture.componentInstance;
    component.listing = [];
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
