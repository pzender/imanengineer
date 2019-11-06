import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllChannelsComponent } from './all-channels.component';
import { FilterSidebarComponent } from 'src/app/shared/components/filter-sidebar/filter-sidebar.component';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { OfferPickerComponent } from 'src/app/shared/components/offer-picker/offer-picker.component';

describe('AllChannelsComponent', () => {
  let component: AllChannelsComponent;
  let fixture: ComponentFixture<AllChannelsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllChannelsComponent, FilterSidebarComponent, OfferPickerComponent ],
      imports: [RouterTestingModule, FormsModule, HttpClientTestingModule]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllChannelsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
