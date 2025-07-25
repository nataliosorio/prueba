using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Services;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Business.Services
{
    public class OrdersBusiness
    {
        private readonly OrdersData _ordersData;
        private readonly ILogger<OrdersBusiness> _logger;

        public OrdersBusiness(OrdersData ordersData, ILogger<OrdersBusiness> logger)
        {
            _ordersData = ordersData;
            _logger = logger;
        }

        public async Task<IEnumerable<Orders>> GetAllAsync()
        {
            return await _ordersData.GetAllWithDetailsAsync();
        }

        public async Task<IEnumerable<OrdersDto>> GetAllDtoAsync()
        {
            try
            {
                var orders = await _ordersData.GetAllWithDetailsAsync();

                return orders.Select(o => new OrdersDto
                {
                    Id = o.Id,
                    Fecha = o.Fecha,
                    Estado = o.Estado,
                    ClienteId = o.ClienteId,
                    ClienteNombre = o.Customer?.Nombre,
                    PizzaId = o.PizzaId,
                    PizzaNombre = o.Pizza?.Name,
                    PizzaPrecio = o.Pizza?.Price ?? 0,
                    Cantidad = o.Cantidad
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al mapear los pedidos a DTO");
                throw;
            }
        }

        public async Task<OrdersDto?> GetByIdDtoAsync(int id)
        {
            var order = await _ordersData.GetByIdWithDetailsAsync(id);

            if (order == null)
                return null;

            return new OrdersDto
            {
                Id = order.Id,
                Fecha = order.Fecha,
                Estado = order.Estado,
                ClienteId = order.ClienteId,
                ClienteNombre = order.Customer?.Nombre,
                PizzaId = order.PizzaId,
                PizzaNombre = order.Pizza?.Name,
                PizzaPrecio = order.Pizza?.Price ?? 0,
                Cantidad = order.Cantidad
            };
        }

        public async Task<Orders> CreateAsync(CreateOrderDto dto)
        {
            var order = new Orders
            {
                Fecha = DateTime.Now,
                Estado = "Pendiente",
                ClienteId = dto.ClienteId,
                PizzaId = dto.PizzaId,
                Cantidad = dto.Cantidad
            };

            return await _ordersData.CreateAsync(order);
        }

        public async Task<bool> UpdateAsync(OrdersDto dto)
        {
            var order = new Orders
            {
                Id = dto.Id,
                Fecha = dto.Fecha,
                Estado = dto.Estado,
                ClienteId = dto.ClienteId,
                PizzaId = dto.PizzaId,
                Cantidad = dto.Cantidad
            };

            return await _ordersData.UpdateAsync(order);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _ordersData.DeleteAsync(id);
        }

        public async Task<bool> DeleteLogicAsync(int id)
        {
            return await _ordersData.DeleteLogicAsync(id);
        }

        public async Task<bool> RecoverLogicAsync(int id)
        {
            return await _ordersData.RecoverLogicAsync(id);
        }
    }
}
