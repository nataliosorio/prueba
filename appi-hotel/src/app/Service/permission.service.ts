import { Injectable } from '@angular/core';
import { PruebaServiceService } from '../prueba-service.service';
import { Observable } from 'rxjs';
import { Permission } from '../Interfaces/permission';

@Injectable({
  providedIn: 'root'
})
export class PermissionService {

 private endpoint = 'Permission';

constructor(private api: PruebaServiceService) {}
  
    getAll(): Observable<Permission[]> {
      return this.api.get<Permission[]>(this.endpoint);
    }
  
    getById(id: number): Observable<Permission> {
      return this.api.getById<Permission>(this.endpoint, id);
    }
  
    create(data: Permission): Observable<Permission> {
      return this.api.post<Permission>(this.endpoint, data);
    }
  
    update(id: number, data: Permission): Observable<Permission> {
      return this.api.put<Permission>(this.endpoint, id, data);
    }
  
    delete(id: number): Observable<any> {
      return this.api.delete(this.endpoint, id);
    }

    deleteLogic(id: number): Observable<Permission> {
      return this.api.deleteLogic<Permission>(this.endpoint, id);
    }

     restore(id: number): Observable<Permission> {
          return this.api.patchRestore<Permission>(this.endpoint, id);
    }
}
