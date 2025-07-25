import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { LoginRequest } from './Interfaces/login-request';
import { LoginResponse } from './Interfaces/login-response';
import { jwtDecode } from 'jwt-decode';
import { googleToken } from './Interfaces/googleToken';
import { RegisterDto } from './Interfaces/register-dto';



@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  private apiUrl = 'https://localhost:7205/api/Auth';
  constructor(private http: HttpClient) {}

  login(data: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, data).pipe(
      tap(res => {
        // console.log('Token recibido del backend:', res.token);
        localStorage.setItem('token', res.token);
        localStorage.setItem('roles', JSON.stringify(res.roles));
      })
    );
  }

  register(data: RegisterDto): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/register`, data).pipe(
      tap(res => {
        // console.log('Token recibido del backend:', res.token);
        localStorage.setItem('token', res.token);
        localStorage.setItem('roles', JSON.stringify(res.roles));
      })
    );
  }

  googleLogin(tokenDto: googleToken): Observable<LoginResponse> {
  return this.http.post<LoginResponse>(`${this.apiUrl}/google-login`, tokenDto).pipe(
    tap(res => {

        localStorage.setItem('token', res.token);
        localStorage.setItem('roles', JSON.stringify(res.roles));

      })
  );
}



  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('roles');
  }

  // getToken() {
  //   return localStorage.getItem('token');
  // }

  getToken(): string | null {
  return localStorage.getItem('token');
}

  isAdmin(): boolean {
    const roles = JSON.parse(localStorage.getItem('roles') || '[]');
    return roles.includes('Administrador');
  }

    isAsistente(): boolean {
    const roles = JSON.parse(localStorage.getItem('roles') || '[]');
    return roles.includes('Asistente');
  }

    isPizzero(): boolean {
    const roles = JSON.parse(localStorage.getItem('roles') || '[]');
    return roles.includes('Pizzero');
  }

   // Decodificar el token JWT y obtener los datos que contiene
   decodeToken(): any {
    const token = this.getToken();
    // console.log('Token recuperado:', token)
    if (!token) {
      return null; // Si no hay token, no se puede decodificar
    }
    try {
      const decoded = jwtDecode(token);
      console.log('Payload del token decodificado:', decoded);
      return decoded;
    } catch (error) {
      console.error('Error decodificando el token', error);
      return null;
    }
  }

    getUsername(): string | null {
    const payload = this.decodeToken();
    return payload?.unique_name || null;
  }

  getUserRole(): string | null {
  const payload = this.decodeToken();
  return payload?.role || null;
}


  // Obtener la fecha de expiración del token
  getTokenExpiration(): number | null {
    const decodedToken = this.decodeToken();
    return decodedToken ? decodedToken.exp : null; // Retorna el 'exp' (expiración) si existe
  }

  // Verificar si el token ha expirado
  isTokenExpired(): boolean {
    const expiration = this.getTokenExpiration();
    if (!expiration) {
      return true; // Si no tiene expiración, consideramos que ha expirado
    }
    const currentTime = Math.floor(Date.now() / 1000); // Obtener el tiempo actual en segundos
    return currentTime >= expiration; // Si el tiempo actual es mayor o igual a la fecha de expiración, el token ha expirado
  }
}




