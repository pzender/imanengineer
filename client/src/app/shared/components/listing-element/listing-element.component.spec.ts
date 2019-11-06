import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListingElementComponent } from './listing-element.component';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ServiceWorkerModule } from '@angular/service-worker';

describe('ListingElementComponent', () => {
  let component: ListingElementComponent;
  let fixture: ComponentFixture<ListingElementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListingElementComponent ],
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
    fixture = TestBed.createComponent(ListingElementComponent);
    component = fixture.componentInstance;
    component.programme = {
      id: 1,
      title: 'test',
      description: 'test',
      emissions: [],
      features: [],
      iconUrl: '',
      rating: undefined
    }
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
