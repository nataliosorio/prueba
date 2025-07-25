import { Injectable } from '@angular/core';
import { PruebaServiceService } from '../prueba-service.service';
import { Form } from '../Interfaces/form';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FormService {

  private endpoint = 'FormControllerPrueba';
  
    constructor(private api: PruebaServiceService) {}
  
    getAll(): Observable<Form[]> {
      return this.api.get<Form[]>(this.endpoint);
    }
  
    getById(id: number): Observable<Form> {
      return this.api.getById<Form>(this.endpoint, id);
    }
  
    create(data: Form): Observable<Form> {
      return this.api.post<Form>(this.endpoint, data);
    }
  
    update(id: number, data: Form): Observable<Form> {
      return this.api.put<Form>(this.endpoint, id, data);
    }
  
    delete(id: number): Observable<any> {
      return this.api.delete(this.endpoint, id);
    }

    deleteLogic(id: number): Observable<Form> {
      return this.api.deleteLogic<Form>(this.endpoint, id);
    }

    // restore(id: number): Observable<Form> {
    //   return this.api.patchRestore<Form>(this.endpoint, id, {});
    // }

    restore(id: number): Observable<Form> {
      return this.api.patchRestore<Form>(this.endpoint, id);
    }
  
}
