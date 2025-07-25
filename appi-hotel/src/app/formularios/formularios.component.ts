import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormService } from '../Service/form.service';
import { Form } from '../Interfaces/form';
import { finalize } from 'rxjs';
import { AuthServiceService } from '../auth-service.service';

@Component({
  selector: 'app-formularios',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule ],
  templateUrl: './formularios.component.html',
  styleUrl: './formularios.component.css'
})
export class FormulariosComponent {
  formForm!: FormGroup;
  forms: Form[] = [];
  isLoading = false;
  showForm = false;
  searchId: number | null = null;
  esAdmin: boolean = false;

  constructor(
    private fb: FormBuilder,
    private formService: FormService,
    private authService: AuthServiceService
  ) {}

  ngOnInit(): void {
    this.loadForms();

    this.formForm = this.fb.group({
      id: [null], // <- importante para manejar edición
      name: ['', Validators.required],
      description: ['', Validators.required],
      active: [false]
    });

    this.esAdmin = this.authService.isAdmin();
  }

  loadForms(): void {
    this.isLoading = true;
    this.formService.getAll()
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: (data) => this.forms = data,
        error: (err) => console.error('Error al cargar formularios:', err)
      });
  }

  getFormById(): void {
    if (this.searchId !== null && this.searchId > 0) {
      this.isLoading = true;
      this.formService.getById(this.searchId)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: (form) => {
            this.forms = [form]; // Muestra solo ese resultado en la tabla
          },
          error: (err) => {
            console.error('Formulario no encontrado:', err);
            this.forms = []; // Limpia si no se encuentra
          }
        });
    }
  }

  submitForm(): void {
    if (this.formForm.invalid) return;

    const formData = this.formForm.value;
    this.isLoading = true;

    if (formData.id) {
      // Es edición
      this.formService.update(formData.id, formData)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: () => {
            this.loadForms(); // ✅ Recarga desde el backend
            this.closeForm();
          },
          error: (err) => console.error('Error al actualizar formulario:', err)
        });
    } else {
      // Es creación
      const { id, ...newForm } = formData; // Remover id antes de enviar
      this.formService.create(newForm as Form)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: (created) => {
            this.forms.push(created);
            this.closeForm();
          },
          error: (err) => console.error('Error al crear formulario:', err)
        });
    }
  }

  editForm(index: number): void {
    const form = this.forms[index];
    this.formForm.patchValue(form);
    this.showForm = true;
  }

  deleteForm(id: number): void {
    const confirmado = window.confirm('¿Estás seguro de que deseas eliminar este formulario permanentemente?');
  
    if (!confirmado) return;
  
    this.isLoading = true;
  
    this.formService.delete(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.forms = this.forms.filter(f => f.id !== id);
        },
        error: (err) => console.error('Error al eliminar formulario:', err)
      });
  }


  deleteLogic(id: number): void {
    const confirmado = window.confirm('¿Estás seguro de que deseas eliminar este formulario de forma lógica?');
  
    if (!confirmado) return;
  
    this.isLoading = true;
  
    this.formService.deleteLogic(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.loadForms(); 
        },
        error: (err) => console.error('Error al realizar eliminación lógica:', err)
      });
  }



  recuperarEliminados(id: number): void {
    const confirmado = window.confirm('¿Deseas recuperar este formulario eliminado?');
  
    if (!confirmado) return;
  
    this.isLoading = true;
  
    this.formService.restore(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.loadForms(); 
        },
        error: (err) => console.error('Error al restaurar el formulario:', err)
      });
  }

  toggleForm(): void {
    this.showForm = !this.showForm;
    if (!this.showForm) this.formForm.reset({ id: null, active: false });
  }

  closeForm(): void {
    this.showForm = false;
    this.formForm.reset({ id: null, active: false });
  }

  trackById(index: number, item: Form): number {
    return item.id;
  }
}
