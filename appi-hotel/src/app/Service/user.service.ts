import { Injectable } from '@angular/core';
import { PruebaServiceService } from '../prueba-service.service';
import { catchError, Observable, throwError } from 'rxjs';
import { User } from '../Interfaces/user';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = 'https://localhost:7205/api/User';
 // cambia esto según tu URL real

  
  // cambia esto según tu URL real

  constructor(private http: HttpClient) {}

  // Método para manejar errores HTTP
  private handleError(error: HttpErrorResponse) {
    console.error('Error HTTP:', error);
    return throwError(() => new Error('Ocurrió un error en la petición. Por favor intenta de nuevo.'));
  }

  getRooms(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl)
      .pipe(catchError(this.handleError));
  }

  addRoom(city: Omit<User, 'id'>): Observable<User> {
    return this.http.post<User>(this.apiUrl, city)
      .pipe(catchError(this.handleError));
  }

   update(id: number, data: User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/${id}`, data)
   }


  deleteRoom(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

deleteLogic(id: number | string): Observable<void> {
  const url = `${this.apiUrl}/logic/${id}`;
  console.log('DELETE lógico a:', url); // 💡 Depura aquí
  return this.http.delete<void>(url).pipe(
    catchError(this.handleError)
  );
}

  patchRestore(id: number | string): Observable<User> {
  const url = `${this.apiUrl}/restore/${id}`;
  return this.http.patch<User>(url, {}).pipe(
    catchError(this.handleError)
  );
}




        
}
