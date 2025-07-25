import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Rol } from '../Interfaces/rol';
import { RolService } from '../Service/rol.service';
import { finalize } from 'rxjs';
import { AuthServiceService } from '../auth-service.service';

@Component({
  selector: 'app-roles',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './roles.component.html',
  styleUrl: './roles.component.css'
})
export class RolesComponent {
  rolForm!: FormGroup;
  roles: Rol [] = [];
  isLoading = false;
  showForm = false;
  searchId: number | null = null;
  esAdmin: boolean = false;


  constructor(
    private fb: FormBuilder,
    private RolService: RolService,
        private authService: AuthServiceService
    
  ) {}

  ngOnInit(): void {
    this.loadRoles();

    this.rolForm = this.fb.group({
      id: [null], // <- importante para manejar edición
      name: ['', Validators.required],
      description: ['', Validators.required],
      active: [false]
    });
    this.esAdmin = this.authService.isAdmin();

  }

  loadRoles(): void {
    this.isLoading = true;
    this.RolService.getAll()
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: (data) => this.roles = data,
        error: (err) => console.error('Error al cargar los modulos:', err)
      });
  }



  getRolById(): void {
    if (this.searchId !== null && this.searchId > 0) {
      this.isLoading = true;
      this.RolService.getById(this.searchId)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: (form) => {
            this.roles = [form]; 
          },
          error: (err) => {
            console.error('Módulo no encontrado:', err);
            this.roles = []; 
          }
        });
    }
  }

  submitRol(): void {
    if (this.rolForm.invalid) return;

    const formData = this.rolForm.value;
    this.isLoading = true;

    if (formData.id) {
      // Es edición
      this.RolService.update(formData.id, formData)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: () => {
            this.loadRoles(); 
            this.closeForm();
          },
          error: (err) => console.error('Error al actualizar módulo:', err)
        });
    } else {
      // Es creación
      const { id, ...newForm } = formData; // Remover id antes de enviar
      this.RolService.create(newForm as Rol)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: (created) => {
            this.roles.push(created);
            this.closeForm();
          },
          error: (err) => console.error('Error al crear módulo:', err)
        });
    }
  }

  editRol(index: number): void {
    const form = this.roles[index];
    this.rolForm.patchValue(form);
    this.showForm = true;
  }

  deleteRol(id: number): void {
    const confirmado = window.confirm('¿Estás seguro de que deseas eliminar este Rol permanentemente?');
    if (!confirmado) return;

    this.isLoading = true;
    this.RolService.delete(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.roles = this.roles.filter(f => f.id !== id);
        },
        error: (err) => console.error('Error al eliminar módulo:', err)
      });
  }

 

  deleteLogic(id: number): void {
    const confirmado = window.confirm('¿Estás seguro de que deseas eliminar  este Rol?');
    if (!confirmado) return;
    this.isLoading = true;
  
    this.RolService.deleteLogic(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.loadRoles(); 
        },
        error: (err) => console.error('Error al realizar eliminación lógica:', err)
      });
  }

  recuperarEliminados(id: number): void {
    const confirmado = window.confirm('¿Estás seguro de que deseas recuperar este Rol eliminado?');
    if (!confirmado) return;
    this.isLoading = true;
  
    this.RolService.restore(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.loadRoles(); // Recarga la tabla para mostrar el registro restaurado
        },
        error: (err) => console.error('Error al restaurar el formulario:', err)
      });
  }

  toggleForm(): void {
    this.showForm = !this.showForm;
    if (!this.showForm) this.rolForm.reset({ id: null, active: false });
  }

  closeForm(): void {
    this.showForm = false;
    this.rolForm.reset({ id: null, active: false });
  }

  trackById(index: number, item: Rol): number {
    return item.id;
  }
}
