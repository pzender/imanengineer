import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OfferPickerComponent } from './offer-picker.component';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ServiceWorkerModule } from '@angular/service-worker';

describe('OfferPickerComponent', () => {
  let component: OfferPickerComponent;
  let fixture: ComponentFixture<OfferPickerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OfferPickerComponent ],
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
    fixture = TestBed.createComponent(OfferPickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
