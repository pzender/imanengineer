import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListingElementComponent } from './listing-element.component';

describe('ListingElementComponent', () => {
  let component: ListingElementComponent;
  let fixture: ComponentFixture<ListingElementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListingElementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListingElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
