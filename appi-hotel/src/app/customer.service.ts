import { Injectable } from '@angular/core';
import { PruebaServiceService } from './prueba-service.service';
import { Customer } from './Interfaces/customer';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

   private endpoint = 'Customer'; // Asegúrate que coincida con tu controlador API

      constructor(private api: PruebaServiceService) {}

      getAll(): Observable<Customer[]> {
        return this.api.get<Customer[]>(this.endpoint);
      }

      getById(id: number): Observable<Customer> {
        return this.api.getById<Customer>(this.endpoint, id);
      }
}
