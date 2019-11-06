import { TestBed } from '@angular/core/testing';

import { AllChannelsService } from './all-channels.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';


describe('AllChannelsService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientTestingModule]
  }));

  it('should be created', () => {
    const service: AllChannelsService = TestBed.get(AllChannelsService);
    expect(service).toBeTruthy();
  });
});
