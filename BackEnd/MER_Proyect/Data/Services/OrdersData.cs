using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Services
{
    public class OrdersData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrdersData> _logger;

        public OrdersData(ApplicationDbContext context, ILogger<OrdersData> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Orders>> GetAllWithDetailsAsync()
        {
            try
            {
                return await _context.orders
                    .Include(o => o.Customer)
                    .Include(o => o.Pizza)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los pedidos con cliente y pizza");
                throw;
            }
        }

        public async Task<Orders?> GetByIdWithDetailsAsync(int id)
        {
            try
            {
                return await _context.orders
                    .Include(o => o.Customer)
                    .Include(o => o.Pizza)
                    .FirstOrDefaultAsync(o => o.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el pedido con ID {Id}", id);
                throw;
            }
        }

        public async Task<Orders> CreateAsync(Orders order)
        {
            try
            {
                await _context.orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el pedido");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Orders order)
        {
            try
            {
                _context.orders.Update(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el pedido");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var order = await _context.orders.FindAsync(id);
                if (order == null)
                    return false;

                _context.orders.Remove(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el pedido");
                return false;
            }
        }

        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var order = await GetByIdWithDetailsAsync(id);
                if (order == null)
                    return false;

                order.Estado = "Inactivo"; // o "Cancelado"
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar lógicamente el pedido: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RecoverLogicAsync(int id)
        {
            try
            {
                var order = await GetByIdWithDetailsAsync(id);
                if (order == null)
                    return false;

                order.Estado = "Pendiente";
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al recuperar el pedido: {ex.Message}");
                return false;
            }
        }
    }
}
