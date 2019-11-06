import { TestBed, async, inject } from '@angular/core/testing';

import { UserKnownGuard } from './user-known.guard';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ServiceWorkerModule } from '@angular/service-worker';

describe('UserKnownGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserKnownGuard],
      imports: [
        HttpClientTestingModule, 
        RouterTestingModule,
        ServiceWorkerModule.register('', {enabled: false})
      ]

    });
  });

  it('should create', inject([UserKnownGuard], (guard: UserKnownGuard) => {
    expect(guard).toBeTruthy();
  }));
});
