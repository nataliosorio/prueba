import { Injectable } from '@angular/core';
import { PruebaServiceService } from '../prueba-service.service';
import { Observable } from 'rxjs';
import { Person } from '../Interfaces/person';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

 private endpoint = 'Person';
 
 constructor(private api: PruebaServiceService) {}
   
     getAll(): Observable<Person[]> {
       return this.api.get<Person[]>(this.endpoint);
     }
   
     getById(id: number): Observable<Person> {
       return this.api.getById<Person>(this.endpoint, id);
     }
   
     create(data: Person): Observable<Person> {
       return this.api.post<Person>(this.endpoint, data);
     }
   
     update(id: number, data: Person): Observable<Person> {
       return this.api.put<Person>(this.endpoint, id, data);
     }
   
     delete(id: number): Observable<any> {
       return this.api.delete(this.endpoint, id);
     }

     
    deleteLogic(id: number): Observable<Person> {
      return this.api.deleteLogic<Person>(this.endpoint, id);
    }

     restore(id: number): Observable<Person> {
          return this.api.patchRestore<Person>(this.endpoint, id);
    }
}
