import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Permission } from '../Interfaces/permission';
import { PermissionService } from '../Service/permission.service';
import { finalize } from 'rxjs';
import { AuthServiceService } from '../auth-service.service';

@Component({
  selector: 'app-permisos',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './permisos.component.html',
  styleUrl: './permisos.component.css'
})
export class PermisosComponent {
  permissionForm!: FormGroup;
  permissions: Permission [] = [];
  isLoading = false;
  showForm = false;
  searchId: number | null = null;
  esAdmin: boolean = false;


  constructor(
    private fb: FormBuilder,
    private permisionService: PermissionService,
        private authService: AuthServiceService
    
  ) {}

  ngOnInit(): void {
    this.loadPermissions();

    this.permissionForm = this.fb.group({
      id: [null], // <- importante para manejar edición
      name: ['', Validators.required],
      description: ['', Validators.required],
      active: [false]
    });

    this.esAdmin = this.authService.isAdmin();
  }

  loadPermissions(): void {
    this.isLoading = true;
    this.permisionService.getAll()
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: (data) => this.permissions = data,
        error: (err) => console.error('Error al cargar formularios:', err)
      });
  }

  getPermissionById(): void {
    if (this.searchId !== null && this.searchId > 0) {
      this.isLoading = true;
      this.permisionService.getById(this.searchId)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: (form) => {
            this.permissions = [form]; // Muestra solo ese resultado en la tabla
          },
          error: (err) => {
            console.error('Formulario no encontrado:', err);
            this.permissions = []; // Limpia si no se encuentra
          }
        });
    }
  }

  submitPermission(): void {
    if (this.permissionForm.invalid) return;

    const formData = this.permissionForm.value;
    this.isLoading = true;

    if (formData.id) {
      // Es edición
      this.permisionService.update(formData.id, formData)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: () => {
            this.loadPermissions(); // ✅ Recarga desde el backend
            this.closeForm();
          },
          error: (err) => console.error('Error al actualizar formulario:', err)
        });
    } else {
      // Es creación
      const { id, ...newForm } = formData; // Remover id antes de enviar
      this.permisionService.create(newForm as Permission)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: (created) => {
            this.permissions.push(created);
            this.closeForm();
          },
          error: (err) => console.error('Error al crear formulario:', err)
        });
    }
  }

  editPermission(index: number): void {
    const form = this.permissions[index];
    this.permissionForm.patchValue(form);
    this.showForm = true;
  }

  // deletePermission(id: number): void {
  //   this.isLoading = true;
  //   this.permisionService.delete(id)
  //     .pipe(finalize(() => this.isLoading = false))
  //     .subscribe({
  //       next: () => {
  //         this.permissions = this.permissions.filter(f => f.id !== id);
  //       },
  //       error: (err) => console.error('Error al eliminar formulario:', err)
  //     });
  // }


  deletePermission(id: number): void {
    const confirmado = window.confirm('¿Estás seguro de que deseas eliminar este permiso?');
  
    if (!confirmado) return;
  
    this.isLoading = true;
  
    this.permisionService.delete(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.permissions = this.permissions.filter(f => f.id !== id);
        },
        error: (err) => console.error('Error al eliminar permiso:', err)
      });
  }

  // deleteLogic(id: number): void {
  //   this.isLoading = true;
  
  //   this.permisionService.deleteLogic(id)
  //     .pipe(finalize(() => this.isLoading = false))
  //     .subscribe({
  //       next: () => {
  //         this.loadPermissions(); // Recarga la tabla para reflejar los cambios
  //       },
  //       error: (err) => console.error('Error al realizar eliminación lógica:', err)
  //     });
  // }

  deleteLogic(id: number): void {
    const confirmado = window.confirm('¿Estás seguro de que deseas eliminar lógicamente este permiso?');
  
    if (!confirmado) return;
  
    this.isLoading = true;
  
    this.permisionService.deleteLogic(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.loadPermissions(); // Recarga la tabla para reflejar los cambios
        },
        error: (err) => console.error('Error al realizar eliminación lógica:', err)
      });
  }

  // recuperarEliminados(id: number): void {
  //   this.isLoading = true;
  
  //   this.permisionService.restore(id)
  //     .pipe(finalize(() => this.isLoading = false))
  //     .subscribe({
  //       next: () => {
  //         this.loadPermissions(); // Recarga la tabla para mostrar el registro restaurado
  //       },
  //       error: (err) => console.error('Error al restaurar el formulario:', err)
  //     });
  // }


  recuperarEliminados(id: number): void {
    const confirmado = window.confirm('¿Estás seguro de que deseas restaurar este permiso eliminado?');
  
    if (!confirmado) return;
  
    this.isLoading = true;
  
    this.permisionService.restore(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.loadPermissions(); // Recarga la tabla para mostrar el registro restaurado
        },
        error: (err) => console.error('Error al restaurar el permiso:', err)
      });
  }

  toggleForm(): void {
    this.showForm = !this.showForm;
    if (!this.showForm) this.permissionForm.reset({ id: null, active: false });
  }

  closeForm(): void {
    this.showForm = false;
    this.permissionForm.reset({ id: null, active: false });
  }

  trackById(index: number, item: Permission): number {
    return item.id;
  }
}
