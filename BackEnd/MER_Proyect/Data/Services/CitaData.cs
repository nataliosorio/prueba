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
    public class CitaData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CitaData> _logger;

        public CitaData(ApplicationDbContext context, ILogger<CitaData> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Cita>> GetAllWithDetailsAsync()
        {
            try
            {
                return await _context.Cita
                    .Include(c => c.Paciente)
                    .Include(c => c.Doctor)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las citas con información de paciente y doctor");
                throw;
            }
        }

        public async Task<Cita?> GetByIdWithDetailsAsync(int id)
        {
            try
            {
                return await _context.Cita
                    .Include(c => c.Paciente)
                    .Include(c => c.Doctor)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la cita con ID {Id}", id);
                throw;
            }
        }

        public async Task<Cita> CreateAsync(Cita cita)
        {
            try
            {
                await _context.Cita.AddAsync(cita);
                await _context.SaveChangesAsync();
                return cita;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la cita");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Cita cita)
        {
            try
            {
                _context.Cita.Update(cita);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la cita");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var cita = await _context.Cita.FindAsync(id);
                if (cita == null)
                    return false;

                _context.Cita.Remove(cita);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la cita");
                return false;
            }
        }

        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var cita = await GetByIdWithDetailsAsync(id);
                if (cita == null)
                    return false;

                cita.Active = false;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar lógicamente la cita: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RecoverLogicAsync(int id)
        {
            try
            {
                var cita = await GetByIdWithDetailsAsync(id);
                if (cita == null)
                    return false;

                cita.Active = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al recuperar la cita: {ex.Message}");
                return false;
            }
        }



    }

}
