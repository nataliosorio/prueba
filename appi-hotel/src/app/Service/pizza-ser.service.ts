import { Injectable } from '@angular/core';
import { PruebaServiceService } from '../prueba-service.service';
import { Pizza } from '../Interfaces/pizzas';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PizzaSerService {

   private endpoint = 'Pizzas'; // Asegúrate que coincida con tu controlador API

  constructor(private api: PruebaServiceService) {}

  getAll(): Observable<Pizza[]> {
    return this.api.get<Pizza[]>(this.endpoint);
  }

  getById(id: number): Observable<Pizza> {
    return this.api.getById<Pizza>(this.endpoint, id);
  }

  create(data: Pizza): Observable<Pizza> {
    return this.api.post<Pizza>(this.endpoint, data);
  }

  update(id: number, data: Pizza): Observable<Pizza> {
    return this.api.put<Pizza>(this.endpoint, id, data);
  }

  delete(id: number): Observable<any> {
    return this.api.delete(this.endpoint, id);
  }

  deleteLogic(id: number): Observable<Pizza> {
    return this.api.deleteLogic<Pizza>(this.endpoint, id);
  }

  restore(id: number): Observable<Pizza> {
    return this.api.patchRestore<Pizza>(this.endpoint, id);
  }
}
