import { TestBed } from '@angular/core/testing';

import { ProfileService } from './profile.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ServiceWorkerModule } from '@angular/service-worker';

describe('ProfileService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule,
      ServiceWorkerModule.register('', {enabled: false})
    ]
  }));

  it('should be created', () => {
    const service: ProfileService = TestBed.get(ProfileService);
    expect(service).toBeTruthy();
  });
});
