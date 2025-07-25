import { Routes } from '@angular/router';
import { FormulariosComponent } from './formularios/formularios.component';
import { ModulosComponent } from './modulos/modulos.component';
import { PermisosComponent } from './permisos/permisos.component';
import { RolesComponent } from './roles/roles.component';
import { PersonaComponent } from './persona/persona.component';
import { UsuarioComponent } from './usuario/usuario.component';
import { RolUsuarioComponent } from './rol-usuario/rol-usuario.component';
import { ForRolPermisosComponent } from './for-rol-permisos/for-rol-permisos.component';
import { MenuComponent } from './menu/menu.component';
import { LoginComponent } from './login/login.component';
import { AuthGuardService } from './auth-guard.service';
import { PizzasComponent } from './pizzas/pizzas.component';
import { OrdenesComponent } from './ordenes/ordenes.component';

export const routes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' }, // Redirección inicial
    { path: 'login', component: LoginComponent },
    {
        path: 'home',
        component: MenuComponent,
        canActivate: [AuthGuardService],
        children: [
          { path: 'Pizzas', component: PizzasComponent, canActivate: [AuthGuardService] },
          { path: 'Ordenes', component: OrdenesComponent, canActivate: [AuthGuardService] },

          { path: 'Formularios', component: FormulariosComponent, canActivate: [AuthGuardService] },
          { path: 'Citas', component: ModulosComponent,canActivate: [AuthGuardService] },
          // { path: 'Permissions', component: PermisosComponent, canActivate: [AuthGuardService] },
          // { path: 'Roles', component: RolesComponent, canActivate: [AuthGuardService] },
          // { path: 'Persons', component: PersonaComponent, canActivate: [AuthGuardService] },
          { path: 'Users', component: UsuarioComponent, canActivate: [AuthGuardService] },
          // { path: 'RolUser', component: RolUsuarioComponent,canActivate: [AuthGuardService] },
          // { path: 'RolFormPermission', component: ForRolPermisosComponent,canActivate: [AuthGuardService] }
        ]
      },

      { path: '**', redirectTo: 'login' } // Ruta comodín para rutas no encontradas





];
