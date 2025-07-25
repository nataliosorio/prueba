import { Injectable } from '@angular/core';
import { PruebaServiceService } from '../prueba-service.service';
import { Observable } from 'rxjs';
import { Rol } from '../Interfaces/rol';

@Injectable({
  providedIn: 'root'
})
export class RolService {

   private endpoint = 'Rol';
    
      constructor(private api: PruebaServiceService) {}
    
      getAll(): Observable<Rol[]> {
        return this.api.get<Rol[]>(this.endpoint);
      }
    
      getById(id: number): Observable<Rol> {
        return this.api.getById<Rol>(this.endpoint, id);
      }
    
      create(data: Rol): Observable<Rol> {
        return this.api.post<Rol>(this.endpoint, data);
      }
    
      update(id: number, data: Rol): Observable<Rol> {
        return this.api.put<Rol>(this.endpoint, id, data);
      }
    
      delete(id: number): Observable<any> {
        return this.api.delete(this.endpoint, id);
      }

       deleteLogic(id: number): Observable<Rol> {
            return this.api.deleteLogic<Rol>(this.endpoint, id);
      }

    restore(id: number): Observable<Rol> {
        return this.api.patchRestore<Rol>(this.endpoint, id);
      }
}
