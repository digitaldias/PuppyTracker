import { TestBed } from '@angular/core/testing';

import { PottyBreakService } from './potty-break.service';

describe('PottyBreakService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PottyBreakService = TestBed.get(PottyBreakService);
    expect(service).toBeTruthy();
  });
});
