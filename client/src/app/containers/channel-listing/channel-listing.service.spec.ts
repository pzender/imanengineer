import { TestBed } from '@angular/core/testing';

import { ChannelListingService } from './channel-listing.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ServiceWorkerModule } from '@angular/service-worker';

describe('ChannelListingService', () => {
  beforeEach(() => TestBed.configureTestingModule({    
    imports: [
      HttpClientTestingModule,
      ServiceWorkerModule.register('', {enabled: false})
    ]
  }));

  it('should be created', () => {
    const service: ChannelListingService = TestBed.get(ChannelListingService);
    expect(service).toBeTruthy();
  });
});
