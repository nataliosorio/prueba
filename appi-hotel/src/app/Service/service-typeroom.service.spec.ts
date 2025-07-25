import { TestBed } from '@angular/core/testing';

import { ServiceTyperoomService } from './service-typeroom.service';

describe('ServiceTyperoomService', () => {
  let service: ServiceTyperoomService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServiceTyperoomService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
