import { Customer } from './customer';
import { Pizza } from './pizzas';

export interface Orders {
  id: number;
  fecha: string; // ISO string format, ej. "2025-07-25T14:30:00Z"
  clienteId: number;
  customer: Customer;
  pizzaId: number;
  pizza: Pizza;
  cantidad: number;
  estado: string; // "Pendiente", "Entregado", etc.
}
