import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Module } from '../Interfaces/module';
import { ModuleService } from '../Service/module.service';
import { finalize } from 'rxjs';
import { AuthServiceService } from '../auth-service.service';
import { Citas } from '../Interfaces/citas';
import { PacienteService } from '../Service/paciente.service';
import { DoctorService } from '../Service/doctor.service';
import { paciente } from '../Interfaces/paciente';
import { doctor } from '../Interfaces/doctor';

@Component({
  selector: 'app-modulos',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule ],
  templateUrl: './modulos.component.html',
  styleUrl: './modulos.component.css'
})
export class ModulosComponent {
  citas: Citas[] = [];
  pacientes: paciente[] = [];
  doctores: doctor[] = [];

  citaForm!: FormGroup;
  showForm: boolean = false;
  editIndex: number | null = null;
  isLoading: boolean = false;
  esAdmin: boolean = false;

  constructor(
    private citasService: ModuleService,
    private pacienteService: PacienteService,
    private doctorService: DoctorService,
    private fb: FormBuilder,
    private authService: AuthServiceService
  ) {}

  ngOnInit(): void {
    this.loadCitas();
    this.loadPacientes();
    this.loadDoctores();

    this.citaForm = this.fb.group({
      Id: [null],
      FechaHora: [null, Validators.required],
      MotivoConsulta: ['', Validators.required],
      PacienteId: [null, Validators.required],
      DoctorId: [null, Validators.required],
      active: [true]
    });

    this.esAdmin = this.authService.isAdmin();
  }

loadCitas(): void {
  this.citasService.getAll().subscribe({
    next: (data) => {
      console.log('Citas cargadas:', data); // <-- Agrega esto para revisar en consola
      this.citas = data;
    },
    error: (err) => {
      console.error('Error al cargar citas:', err);
    }
  });
}

  loadPacientes(): void {
    this.pacienteService.getAll().subscribe({
      next: (data) => this.pacientes = data,
      error: (err) => console.error('Error al cargar pacientes:', err)
    });
  }

  loadDoctores(): void {
    this.doctorService.getAll().subscribe({
      next: (data) => this.doctores = data,
      error: (err) => console.error('Error al cargar doctores:', err)
    });
  }

  refrescar(): void {
    this.loadCitas();
  }

  toggleForm(): void {
    this.showForm = !this.showForm;
    this.citaForm.reset();
    this.editIndex = null;
  }

  closeForm(): void {
    this.showForm = false;
    this.citaForm.reset();
    this.editIndex = null;
  }

  trackById(index: number, item: Citas): number {
    return item.Id;
  }

  submitForm(): void {
    if (this.citaForm.valid) {
      const { Id, ...newCita } = this.citaForm.value;

      this.isLoading = true;
      this.citasService.create(newCita)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: () => {
            this.loadCitas();
            this.closeForm();
          },
          error: (error) => {
            console.error('Error al agregar la cita:', error);
          }
        });
    }
  }

  editCita(index: number): void {
    const selected = this.citas[index];
    this.citaForm.patchValue({
      Id: selected.Id,
      FechaHora: selected.FechaHora,
      MotivoConsulta: selected.MotivoConsulta,
      PacienteId: selected.PacienteId,
      DoctorId: selected.DoctorId,
      active: selected.active
    });
    this.showForm = true;
    this.editIndex = index;
  }

  updateCita(): void {
    if (this.citaForm.valid) {
      const formData = this.citaForm.value;

      this.isLoading = true;
      this.citasService.update(formData.Id, formData)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: () => {
            this.loadCitas();
            this.closeForm();
          },
          error: (error) => {
            console.error('Error al actualizar la cita:', error);
          }
        });
    }
  }

  deleteCita(id: number): void {
    if (confirm('¿Estás seguro de eliminar esta cita?')) {
      this.isLoading = true;
      this.citasService.delete(id)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: () => {
            this.loadCitas();
          },
          error: (error) => {
            console.error('Error al eliminar la cita:', error);
          }
        });
    }
  }

  deleteLogic(id: number): void {
    const confirmado = window.confirm('¿Estás seguro de que deseas eliminar esta cita de forma lógica?');
    if (!confirmado) return;

    this.isLoading = true;
    this.citasService.deleteLogic(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => this.loadCitas(),
        error: (err) => console.error('Error al realizar eliminación lógica:', err)
      });
  }

  recuperarEliminados(id: number): void {
    const confirmado = window.confirm('¿Estás seguro de que deseas restaurar esta cita eliminada?');
    if (!confirmado) return;

    this.isLoading = true;
    this.citasService.restore(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => this.loadCitas(),
        error: (err) => console.error('Error al restaurar la cita:', err)
      });
  }
}
