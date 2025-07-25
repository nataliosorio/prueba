import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginRequest } from '../Interfaces/login-request';
import { AuthServiceService } from '../auth-service.service';
import { IdleServiceService } from '../idle-service.service';
declare const google: any;
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  mostrarRegistro = false;
  login: LoginRequest = {
    username: '',
    password: ''
  };

  registro = {
    Firstname: '',
    Lastname: '',
    Phonenumber: '',
    Username: '',
    Email: '',
    Password: ''
   
  };

  constructor(private authService: AuthServiceService, private idleService: IdleServiceService, private router: Router,private http: HttpClient) {}

  ngOnInit() {

    this.idleService.stopWatching();

    const token = this.authService.getToken();
    if (token && !this.authService.isTokenExpired()) {
      this.router.navigate(['/home']);
    }
    google.accounts.id.initialize({
          client_id: '416045774689-j0aisnaqg13nl949re2nsui7couiqk3p.apps.googleusercontent.com',
          callback: (response: any) => this.handleCredentialResponse(response),
        });

    google.accounts.id.renderButton(
      document.getElementById('googleSignInDiv'),
      { theme: 'outline', size: 'large' }
    );

    // Opcional, muestra prompt automáticamente
    google.accounts.id.prompt();
  }

  onLogin() {
    this.authService.login(this.login).subscribe({
      next: (res) => {
        console.log('Login exitoso');
  
        // Guardar el token recibido
        localStorage.setItem('token', res.token);
  
        // Verificar si el token guardado es válido
        if (this.authService.isTokenExpired()) {
          alert('El token ha expirado');
          return;
        }
  
        // Iniciar seguimiento de inactividad
        this.idleService.startWatching();
  
        // Redirigir al home o dashboard
        this.router.navigate(['/home']);
      },
      error: (err) => {
        console.error('Error de login:', err);
        alert('Credenciales incorrectas');
      }
    });
  }

  handleCredentialResponse(response: any): void {
  const googleToken = response.credential;
const start = performance.now();

  this.authService.googleLogin({ token: googleToken }).subscribe({
    next: (res) => {
      console.log('Login con Google exitoso', res);
        const end = performance.now();
    console.log(`⏱️ Tiempo de respuesta del backend: ${end - start} ms`);
      this.idleService.startWatching();
      this.router.navigate(['/home']);
       console.log(`⏱️ Tiempo de respuesta del backend: ${end - start} ms`);
    },
    error: (err) => {
      console.error('Error en login con Google:', err);
      alert('No se pudo iniciar sesión con Google.');
    }
  });
}

onRegister() {
   this.mostrarRegistro = false;
   this.authService.register(this.registro).subscribe({
      next: (res) => {
        console.log('Cuenta creada exitosamente');
        localStorage.setItem('token', res.token);
        // Verificar si el token guardado es válido
        if (this.authService.isTokenExpired()) {
          alert('El token ha expirado');
          return;
        }

        // Iniciar seguimiento de inactividad
        this.idleService.startWatching();
  
        // Redirigir al home o dashboard
        this.router.navigate(['/home']);
      },
      error: (err) => {
        console.error('Error de login:', err);
        alert('Credenciales incorrectas');
      }
    });
  }

  // onRegister() {
  //   this.authService.login(this.login).subscribe({
  //     next: (res) => {
  //       console.log('Login exitoso');
  
  //       // Guardar el token recibido
  //       localStorage.setItem('token', res.token);
  
  //       // Verificar si el token guardado es válido
  //       if (this.authService.isTokenExpired()) {
  //         alert('El token ha expirado');
  //         return;
  //       }
  
  //       // Iniciar seguimiento de inactividad
  //       this.idleService.startWatching();
  
  //       // Redirigir al home o dashboard
  //       this.router.navigate(['/home']);
  //     },
  //     error: (err) => {
  //       console.error('Error de login:', err);
  //       alert('Credenciales incorrectas');
  //     }
  //   });
  // }
   
  

}
