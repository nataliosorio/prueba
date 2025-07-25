import { Injectable } from '@angular/core';
import { PruebaServiceService } from '../prueba-service.service';
import { Observable } from 'rxjs';
import { Rol } from '../Interfaces/rol';
import { paciente } from '../Interfaces/paciente';

@Injectable({
  providedIn: 'root'
})
export class PacienteService {

   private endpoint = 'Paciente';
    
      constructor(private api: PruebaServiceService) {}
    
      getAll(): Observable<paciente[]> {
        return this.api.get<paciente[]>(this.endpoint);
      }
    
      getById(id: number): Observable<paciente> {
        return this.api.getById<paciente>(this.endpoint, id);
      }
    
    
}
