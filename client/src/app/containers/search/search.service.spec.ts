import { TestBed } from '@angular/core/testing';

import { SearchService } from './search.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ServiceWorkerModule } from '@angular/service-worker';

describe('SearchService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule,
      ServiceWorkerModule.register('', {enabled: false})
    ]
  }));

  it('should be created', () => {
    const service: SearchService = TestBed.get(SearchService);
    expect(service).toBeTruthy();
  });
});
