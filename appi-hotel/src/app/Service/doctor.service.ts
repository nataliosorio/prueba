import { Injectable } from '@angular/core';
import { PruebaServiceService } from '../prueba-service.service';
import { Observable } from 'rxjs';
import { Rol } from '../Interfaces/rol';
import { paciente } from '../Interfaces/paciente';
import { doctor } from '../Interfaces/doctor';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

   private endpoint = 'Doctor';
    
      constructor(private api: PruebaServiceService) {}
    
      getAll(): Observable<doctor[]> {
        return this.api.get<doctor[]>(this.endpoint);
      }
    
      getById(id: number): Observable<doctor> {
        return this.api.getById<doctor>(this.endpoint, id);
      }
    
    
}
