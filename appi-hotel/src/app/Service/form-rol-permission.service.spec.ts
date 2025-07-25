import { TestBed } from '@angular/core/testing';

import { FormRolPermissionService } from './form-rol-permission.service';

describe('FormRolPermissionService', () => {
  let service: FormRolPermissionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FormRolPermissionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
