import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Pizza } from '../Interfaces/pizzas';
import { AuthServiceService } from '../auth-service.service';
import { finalize } from 'rxjs';
import { PizzaSerService } from '../Service/pizza-ser.service';

@Component({
  selector: 'app-pizzas',
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './pizzas.component.html',
  styleUrl: './pizzas.component.css'
})
export class PizzasComponent {
 pizzaForm!: FormGroup;
  pizzas: Pizza[] = [];
  isLoading = false;
  showForm = false;
  esAdmin: boolean = false;

  constructor(
    private fb: FormBuilder,
    private pizzaService: PizzaSerService,
    private authService: AuthServiceService
  ) {}

  ngOnInit(): void {
    this.loadPizzas();

    this.pizzaForm = this.fb.group({
      id: [null],
      name: ['', Validators.required],
      price: [0, [Validators.required, Validators.min(0)]]
    });

    this.esAdmin = this.authService.isAdmin();
  }

   loadPizzas(): void {
    this.isLoading = true;
    this.pizzaService.getAll()
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: (data) => this.pizzas = data,
        error: (err) => console.error('Error al cargar las pizzas:', err)
      });
  }

   submitPizza(): void {
    if (this.pizzaForm.invalid) return;

    const formData = this.pizzaForm.value;
    this.isLoading = true;

    if (formData.id) {
      // Actualización
      this.pizzaService.update(formData.id, formData)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: () => {
            this.loadPizzas();
            this.closeForm();
          },
          error: (err) => console.error('Error al actualizar pizza:', err)
        });
    } else {
      // Creación
      const { id, ...newPizza } = formData;
      this.pizzaService.create(newPizza as Pizza)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: (created) => {
            this.pizzas.push(created);
            this.closeForm();
          },
          error: (err) => console.error('Error al crear pizza:', err)
        });
    }
  }
 editPizza(index: number): void {
    const pizza = this.pizzas[index];
    this.pizzaForm.patchValue(pizza);
    this.showForm = true;
  }

  deletePizza(id: number): void {
    const confirmado = window.confirm('¿Estás seguro de que deseas eliminar esta pizza permanentemente?');
    if (!confirmado) return;

    this.isLoading = true;
    this.pizzaService.delete(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.pizzas = this.pizzas.filter(p => p.id !== id);
        },
        error: (err) => console.error('Error al eliminar pizza:', err)
      });
  }

  toggleForm(): void {
    this.showForm = !this.showForm;
    if (!this.showForm) this.pizzaForm.reset({ id: null, price: 0 });
  }

  closeForm(): void {
    this.showForm = false;
    this.pizzaForm.reset({ id: null, price: 0 });
  }

  trackById(index: number, item: Pizza): number {
    return item.id;
  }

}
