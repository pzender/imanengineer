import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChannelListingComponent } from './channel-listing.component';

describe('ChannelListingComponent', () => {
  let component: ChannelListingComponent;
  let fixture: ComponentFixture<ChannelListingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChannelListingComponent ]
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
