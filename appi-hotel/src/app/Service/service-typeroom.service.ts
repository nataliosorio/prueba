import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { PruebaServiceService } from '../prueba-service.service';

export interface FormControllerPrueba {
  id: number;
  name: string;
  description: string;
  active: boolean;
}


@Injectable({
  providedIn: 'root'
})
export class ServiceTyperoomService {

  private endpoint = 'FormControllerPrueba';

  constructor(private api: PruebaServiceService) {}

  getAll(): Observable<FormControllerPrueba[]> {
    return this.api.get<FormControllerPrueba[]>(this.endpoint);
  }

  getById(id: number): Observable<FormControllerPrueba> {
    return this.api.getById<FormControllerPrueba>(this.endpoint, id);
  }

  create(data: FormControllerPrueba): Observable<FormControllerPrueba> {
    return this.api.post<FormControllerPrueba>(this.endpoint, data);
  }

  update(id: number, data: FormControllerPrueba): Observable<FormControllerPrueba> {
    return this.api.put<FormControllerPrueba>(this.endpoint, id, data);
  }

  delete(id: number): Observable<any> {
    return this.api.delete(this.endpoint, id);
  }
}
