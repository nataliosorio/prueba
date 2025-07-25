import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { Orders } from './Interfaces/orders';
import { CreateOrderDto } from './Interfaces/createOrder';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
 private apiUrl = 'https://localhost:7205/api/Orders'; // Cambia si tu URL es diferente

  constructor(private http: HttpClient) {}

  // Manejo de errores
  private handleError(error: HttpErrorResponse) {
    console.error('Error HTTP:', error);
    return throwError(() => new Error('Ocurrió un error con las órdenes. Intenta nuevamente.'));
  }

  // Obtener todas las órdenes
  getAll(): Observable<Orders[]> {
    return this.http.get<Orders[]>(this.apiUrl)
      .pipe(catchError(this.handleError));
  }

  // Obtener por ID
  getById(id: number): Observable<Orders> {
    return this.http.get<Orders>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  // Crear orden
  create(data: CreateOrderDto): Observable<Orders> {
    return this.http.post<Orders>(this.apiUrl, data)
      .pipe(catchError(this.handleError));
  }

  // Actualizar orden
  update(id: number, data: Orders): Observable<Orders> {
    return this.http.put<Orders>(`${this.apiUrl}/${id}`, data)
      .pipe(catchError(this.handleError));
  }

  // Eliminar permanentemente
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  // Eliminación lógica
  deleteLogic(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/DeleteLogic/${id}`)
      .pipe(catchError(this.handleError));
  }

  // Restaurar orden eliminada
  restore(id: number): Observable<Orders> {
    return this.http.patch<Orders>(`${this.apiUrl}/restore/${id}`, {})
      .pipe(catchError(this.handleError));
  }
}
