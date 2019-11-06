import { TestBed } from '@angular/core/testing';

import { RecommendationsService } from './recommendations.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ServiceWorkerModule } from '@angular/service-worker';

describe('RecommendationsService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule,
      ServiceWorkerModule.register('', {enabled: false})
    ]
  }));

  it('should be created', () => {
    const service: RecommendationsService = TestBed.get(RecommendationsService);
    expect(service).toBeTruthy();
  });
});
