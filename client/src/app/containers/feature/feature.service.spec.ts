import { TestBed } from '@angular/core/testing';

import { FeatureService } from './feature.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ServiceWorkerModule } from '@angular/service-worker';

describe('FeatureService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule,
      ServiceWorkerModule.register('', {enabled: false})
    ]
  }));

  it('should be created', () => {
    const service: FeatureService = TestBed.get(FeatureService);
    expect(service).toBeTruthy();
  });
});
