import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { RolUser } from '../Interfaces/rol-user';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RolUserService {

  private apiUrl = 'https://localhost:7205/api/RolUser'; // cambia esto según tu URL real

  constructor(private http: HttpClient) {}

  // Método para manejar errores HTTP
  private handleError(error: HttpErrorResponse) {
    console.error('Error HTTP:', error);
    return throwError(() => new Error('Ocurrió un error en la petición. Por favor intenta de nuevo.'));
  }

  getRooms(): Observable<RolUser[]> {
    return this.http.get<RolUser[]>(this.apiUrl)
      .pipe(catchError(this.handleError));
  }

  addRoom(city: Omit<RolUser, 'id'>): Observable<RolUser> {
    return this.http.post<RolUser>(this.apiUrl, city)
      .pipe(catchError(this.handleError));
  }

   update(id: number, data: RolUser): Observable<RolUser> {
    return this.http.put<RolUser>(`${this.apiUrl}/${id}`, data)
   }


  deleteRoom(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

 deleteLogic(id: number | string): Observable<void> {
   const url = `${this.apiUrl}/DeleteLogic/${id}`;
   return this.http.delete<void>(url).pipe(
     catchError(this.handleError)
   );
 }
 
   patchRestore(id: number | string): Observable<RolUser> {
   const url = `${this.apiUrl}/restore/${id}`;
   return this.http.patch<RolUser>(url, {}).pipe(
     catchError(this.handleError)
   );
  
      //  restore(id: number): Observable<RolUser> {
      //       return this.http.patch<RolUser>(this.endpoint, id);
      // }
}
}