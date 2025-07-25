import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForRolPermisosComponent } from './for-rol-permisos.component';

describe('ForRolPermisosComponent', () => {
  let component: ForRolPermisosComponent;
  let fixture: ComponentFixture<ForRolPermisosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ForRolPermisosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ForRolPermisosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
