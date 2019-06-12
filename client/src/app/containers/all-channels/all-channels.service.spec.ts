import { TestBed } from '@angular/core/testing';

import { AllChannelsService } from './all-channels.service';

describe('AllChannelsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AllChannelsService = TestBed.get(AllChannelsService);
    expect(service).toBeTruthy();
  });
});
