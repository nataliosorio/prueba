import { Injectable } from '@angular/core';
import { PruebaServiceService } from '../prueba-service.service';
import { Observable } from 'rxjs';
import { Module } from '../Interfaces/module';
import { Citas } from '../Interfaces/citas';

@Injectable({
  providedIn: 'root'
})
export class ModuleService {
  private endpoint = 'Cita';

constructor(private api: PruebaServiceService) {}
  
    getAll(): Observable<Citas[]> {
      return this.api.get<Citas[]>(this.endpoint);
    }
  
    getById(id: number): Observable<Citas> {
      return this.api.getById<Citas>(this.endpoint, id);
    }
  
    create(data: Citas): Observable<Citas> {
      return this.api.post<Citas>(this.endpoint, data);
    }
  
    update(id: number, data: Citas): Observable<Citas> {
      return this.api.put<Citas>(this.endpoint, id, data);
    }
  
    delete(id: number): Observable<any> {
      return this.api.delete(this.endpoint, id);
    }

    deleteLogic(id: number): Observable<Citas> {
      return this.api.deleteLogic<Citas>(this.endpoint, id);
    }

     restore(id: number): Observable<Citas> {
          return this.api.patchRestore<Citas>(this.endpoint, id);
        }
}
