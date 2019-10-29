import { TestBed } from '@angular/core/testing';

import { ProgrammesApiService } from './programmes-api.service';

describe('ProgrammesApiService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProgrammesApiService = TestBed.get(ProgrammesApiService);
    expect(service).toBeTruthy();
  });
});
