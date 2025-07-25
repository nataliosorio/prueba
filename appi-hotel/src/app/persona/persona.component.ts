import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Person } from '../Interfaces/person';
import { PersonService } from '../Service/person.service';
import { finalize } from 'rxjs';
import { AuthServiceService } from '../auth-service.service';

@Component({
  selector: 'app-persona',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './persona.component.html',
  styleUrl: './persona.component.css'
})
export class PersonaComponent {
  personForm!: FormGroup;
  persons: Person [] = [];
  isLoading = false;
  showForm = false;
  searchId: number | null = null;
  esAdmin: boolean = false;

  constructor(
    private fb: FormBuilder,
    private PersonService: PersonService,
            private authService: AuthServiceService
    
  ) {}

  ngOnInit(): void {
    this.loadPersons();

    this.personForm = this.fb.group({
      id: [null], // <- importante para manejar edición
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      phonenumber: ['', Validators.required],
      active: [false]
    });
    this.esAdmin = this.authService.isAdmin();

  }

  loadPersons(): void {
    this.isLoading = true;
    this.PersonService.getAll()
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: (data) => this.persons = data,
        error: (err) => console.error('Error al cargar las personas:', err)
      });
  }



  getPersonById(): void {
    if (this.searchId !== null && this.searchId > 0) {
      this.isLoading = true;
      this.PersonService.getById(this.searchId)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: (form) => {
            this.persons = [form]; 
          },
          error: (err) => {
            console.error('Persona no encontrado:', err);
            this.persons = []; 
          }
        });
    }
  }

  submitPerson(): void {
    if (this.personForm.invalid) return;

    const formData = this.personForm.value;
    this.isLoading = true;

    if (formData.id) {
      // Es edición
      this.PersonService.update(formData.id, formData)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: () => {
            this.loadPersons(); 
            this.closeForm();
          },
          error: (err) => console.error('Error al actualizar person:', err)
        });
    } else {
      // Es creación
      const { id, ...newForm } = formData; // Remover id antes de enviar
      this.PersonService.create(newForm as Person)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: (created) => {
            this.persons.push(created);
            this.closeForm();
          },
          error: (err) => console.error('Error al crear person:', err)
        });
    }
  }

  editPerson(index: number): void {
    const form = this.persons[index];
    this.personForm.patchValue(form);
    this.showForm = true;
  }

  // deletePerson(id: number): void {
  //   this.isLoading = true;
  //   this.PersonService.delete(id)
  //     .pipe(finalize(() => this.isLoading = false))
  //     .subscribe({
  //       next: () => {
  //         this.persons = this.persons.filter(f => f.id !== id);
  //       },
  //       error: (err) => console.error('Error al eliminar person:', err)
  //     });
  // }

  deletePerson(id: number): void {
    const confirmado = window.confirm('¿Estás seguro de que deseas eliminar esta persona?');
  
    if (!confirmado) return;
  
    this.isLoading = true;
  
    this.PersonService.delete(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.persons = this.persons.filter(f => f.id !== id);
        },
        error: (err) => console.error('Error al eliminar la persona:', err)
      });
  }

  // deleteLogic(id: number): void {
  //   this.isLoading = true;
  
  //   this.PersonService.deleteLogic(id)
  //     .pipe(finalize(() => this.isLoading = false))
  //     .subscribe({
  //       next: () => {
  //         this.loadPersons(); 
  //       },
  //       error: (err) => console.error('Error al realizar eliminación lógica:', err)
  //     });
  // }

  deleteLogic(id: number): void {
    const confirmado = window.confirm('¿Estás seguro de que deseas eliminar lógicamente a esta persona?');
  
    if (!confirmado) return;
  
    this.isLoading = true;
  
    this.PersonService.deleteLogic(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.loadPersons(); 
        },
        error: (err) => console.error('Error al realizar eliminación lógica:', err)
      });
  }

  // recuperarEliminados(id: number): void {
  //   this.isLoading = true;
  
  //   this.PersonService.restore(id)
  //     .pipe(finalize(() => this.isLoading = false))
  //     .subscribe({
  //       next: () => {
  //         this.loadPersons(); // Recarga la tabla para mostrar el registro restaurado
  //       },
  //       error: (err) => console.error('Error al restaurar el formulario:', err)
  //     });
  // }

  recuperarEliminados(id: number): void {
    const confirmado = window.confirm('¿Estás seguro de que deseas restaurar este registro de persona?');
  
    if (!confirmado) return;
  
    this.isLoading = true;
  
    this.PersonService.restore(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.loadPersons(); // Recarga la tabla para mostrar el registro restaurado
        },
        error: (err) => console.error('Error al restaurar el formulario:', err)
      });
  }

  toggleForm(): void {
    this.showForm = !this.showForm;
    if (!this.showForm) this.personForm.reset({ id: null, active: false });
  }

  closeForm(): void {
    this.showForm = false;
    this.personForm.reset({ id: null, active: false });
  }

  trackById(index: number, item: Person): number {
    return item.id;
  }
}
