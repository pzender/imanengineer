import { TestBed, async, inject } from '@angular/core/testing';

import { UserKnownGuard } from './user-known.guard';

describe('UserKnownGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserKnownGuard]
    });
  });

  it('should ...', inject([UserKnownGuard], (guard: UserKnownGuard) => {
    expect(guard).toBeTruthy();
  }));
});
