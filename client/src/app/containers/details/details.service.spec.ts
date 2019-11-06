import { TestBed } from '@angular/core/testing';

import { DetailsService } from './details.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ServiceWorkerModule } from '@angular/service-worker';

describe('DetailsService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule,
      ServiceWorkerModule.register('', {enabled: false})
    ]
  }));

  it('should be created', () => {
    const service: DetailsService = TestBed.get(DetailsService);
    expect(service).toBeTruthy();
  });
});
