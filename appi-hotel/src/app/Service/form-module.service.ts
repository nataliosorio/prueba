import { Injectable } from '@angular/core';
import { PruebaServiceService } from '../prueba-service.service';
import { Observable } from 'rxjs';
import { FormModule } from '../Interfaces/form-module';




@Injectable({
  providedIn: 'root'
})
export class FormModuleService {
  private endpoint = 'FormModule';

  constructor(private api: PruebaServiceService) {}

  getAll(): Observable<FormModule[]> {
    return this.api.get<FormModule[]>(this.endpoint);
  }

  getById(id: number): Observable<FormModule> {
    return this.api.getById<FormModule>(this.endpoint, id);
  }

  create(data: FormModule): Observable<FormModule> {
    return this.api.post<FormModule>(this.endpoint, data);
  }

  update(id: number, data: FormModule): Observable<FormModule> {
    return this.api.put<FormModule>(this.endpoint, id, data);
  }

  delete(id: number): Observable<any> {
    return this.api.delete(this.endpoint, id);
  }
}
