import { TestBed } from '@angular/core/testing';

import { PizzaSerService } from './pizza-ser.service';

describe('PizzaSerService', () => {
  let service: PizzaSerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PizzaSerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
