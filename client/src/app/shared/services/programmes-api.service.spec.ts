import { TestBed } from '@angular/core/testing';

import { ProgrammesApiService } from './programmes-api.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ServiceWorkerModule } from '@angular/service-worker';

describe('ProgrammesApiService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule, 
      RouterTestingModule,
      ServiceWorkerModule.register('', {enabled: false})
    ]

  }));

  it('should be created', () => {
    const service: ProgrammesApiService = TestBed.get(ProgrammesApiService);
    expect(service).toBeTruthy();
  });
});
