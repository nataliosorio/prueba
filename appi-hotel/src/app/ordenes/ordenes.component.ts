import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Orders } from '../Interfaces/orders';
import { Pizza } from '../Interfaces/pizzas';
import { Customer } from '../Interfaces/customer';
import { PizzaSerService } from '../Service/pizza-ser.service';
import { CustomerService } from '../customer.service';
import { OrderService } from '../order.service';
import { AuthServiceService } from '../auth-service.service';
import { finalize } from 'rxjs';
import { CreateOrderDto } from '../Interfaces/createOrder';

@Component({
  selector: 'app-ordenes',
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './ordenes.component.html',
  styleUrl: './ordenes.component.css'
})
export class OrdenesComponent {
 ordenes: Orders[] = [];
  pizzas: Pizza[] = [];
  clientes: Customer[] = [];

  orderForm!: FormGroup;
  showForm = false;
  editIndex: number | null = null;
  isLoading = false;
  esAdmin = false;

  constructor(
    private ordersService: OrderService,
    private pizzaService: PizzaSerService,
    private customerService: CustomerService,
    private fb: FormBuilder,
    private authService: AuthServiceService
  ) {}

  ngOnInit(): void {
    this.loadOrdenes();
    this.loadPizzas();
    this.loadClientes();

    this.orderForm = this.fb.group({
      id: [null],
      clienteId: [null, Validators.required],
      pizzaId: [null, Validators.required],
      cantidad: [1, [Validators.required, Validators.min(1)]],
      estado: ['Pendiente', Validators.required]
    });

    this.esAdmin = this.authService.isAdmin();
  }

  loadOrdenes(): void {
    this.isLoading = true;
    this.ordersService.getAll()
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: (data) => this.ordenes = data,
        error: (err) => console.error('Error al cargar órdenes:', err)
      });
  }

  loadPizzas(): void {
    this.pizzaService.getAll().subscribe({
      next: (data) => this.pizzas = data,
      error: (err) => console.error('Error al cargar pizzas:', err)
    });
  }

  loadClientes(): void {
    this.customerService.getAll().subscribe({
      next: (data) => this.clientes = data,
      error: (err) => console.error('Error al cargar clientes:', err)
    });
  }

  toggleForm(): void {
    this.showForm = !this.showForm;
    this.orderForm.reset({ id: null, cantidad: 1, estado: 'Pendiente' });
    this.editIndex = null;
  }

  closeForm(): void {
    this.showForm = false;
    this.orderForm.reset({ id: null, cantidad: 1, estado: 'Pendiente' });
    this.editIndex = null;
  }

  submitForm(): void {
    if (this.orderForm.valid) {
      const dto: CreateOrderDto = {
  clienteId: this.orderForm.value.clienteId,
  pizzaId: this.orderForm.value.pizzaId,
  cantidad: this.orderForm.value.cantidad
};

      // const { id, ...dto }: CreateOrderDto = this.orderForm.value;

      this.isLoading = true;
      this.ordersService.create(dto)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: () => {
            this.loadOrdenes();
            this.closeForm();
          },
          error: (err) => console.error('Error al crear orden:', err)
        });
    }
  }

  editOrder(index: number): void {
    const orden = this.ordenes[index];
    this.orderForm.patchValue({
      id: orden.id,
      clienteId: orden.clienteId,
      pizzaId: orden.pizzaId,
      cantidad: orden.cantidad,
      estado: orden.estado
    });
    this.showForm = true;
    this.editIndex = index;
  }

  updateOrder(): void {
    if (this.orderForm.valid) {
      const formData = this.orderForm.value;

      this.isLoading = true;
      this.ordersService.update(formData.id, formData)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: () => {
            this.loadOrdenes();
            this.closeForm();
          },
          error: (err) => console.error('Error al actualizar orden:', err)
        });
    }
  }

  deleteOrder(id: number): void {
    if (confirm('¿Deseas eliminar esta orden permanentemente?')) {
      this.isLoading = true;
      this.ordersService.delete(id)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: () => this.loadOrdenes(),
          error: (err) => console.error('Error al eliminar orden:', err)
        });
    }
  }

  deleteLogic(id: number): void {
    if (confirm('¿Deseas eliminar lógicamente esta orden?')) {
      this.isLoading = true;
      this.ordersService.deleteLogic(id)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: () => this.loadOrdenes(),
          error: (err) => console.error('Error al eliminar lógicamente la orden:', err)
        });
    }
  }

  recoverOrder(id: number): void {
    if (confirm('¿Deseas recuperar esta orden eliminada?')) {
      this.isLoading = true;
      this.ordersService.restore(id)
        .pipe(finalize(() => this.isLoading = false))
        .subscribe({
          next: () => this.loadOrdenes(),
          error: (err) => console.error('Error al restaurar orden:', err)
        });
    }
  }

  trackByOrdenId(index: number, item: Orders): number {
    return item.id;
  }
}
