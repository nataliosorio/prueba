import { Injectable } from '@angular/core';
import { PruebaServiceService } from '../prueba-service.service';
import { Observable } from 'rxjs';
import { FormModulePermission } from '../Interfaces/form-module-permission';

@Injectable({
  providedIn: 'root'
})
export class FormRolPermissionService {
  private endpoint = 'RolFormPermission';

  constructor(private api: PruebaServiceService) {}

  getAll(): Observable<FormModulePermission[]> {
    return this.api.get<FormModulePermission[]>(this.endpoint);
  }

  getById(id: number): Observable<FormModulePermission> {
    return this.api.getById<FormModulePermission>(this.endpoint, id);
  }

  create(data: FormModulePermission): Observable<FormModulePermission> {
    return this.api.post<FormModulePermission>(this.endpoint, data);
  }

  update(id: number, data: FormModulePermission): Observable<FormModulePermission> {
    return this.api.put<FormModulePermission>(this.endpoint, id, data);
  }

  delete(id: number): Observable<any> {
    return this.api.delete(this.endpoint, id);
  }
}
