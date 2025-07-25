import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthServiceService } from '../auth-service.service';
import { IdleServiceService } from '../idle-service.service';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css'
})
export class MenuComponent {
 username: string | null = null;
  userRole: string | null = null;
  sidebarHidden = false;

  constructor(
    private authService: AuthServiceService, 
    private idleService: IdleServiceService, 
    private router: Router
  ) {}

  ngOnInit() {
     this.username = this.authService.getUsername();
     this.userRole = this.authService.getUserRole();
    // Verificar si el token está presente y no ha expirado
    if (!this.authService.getToken() || this.authService.isTokenExpired()) {
      // Si no hay token o ha expirado, redirigir al login
      this.router.navigate(['/login']);
    } else {
      // Iniciar la verificación de inactividad
      this.idleService.startWatching();
    }
  }

  toggleSidebar() {
    this.sidebarHidden = !this.sidebarHidden;
  }

   logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('roles');
    this.router.navigate(['/login']);
  }

}
