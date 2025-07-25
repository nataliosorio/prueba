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
    public class RolUserData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RolUserData> _logger;

        public RolUserData(ApplicationDbContext context, ILogger<RolUserData> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<RolUser>> GetAllWithUserAndRolAsync()
        {
            try
            {
                return await _context.roluser
                    .Include(ru => ru.User)
                    .Include(ru => ru.Rol)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los registros de RolUser con relaciones");
                throw;
            }
        }



        public async Task<RolUser?> GetByIdWithUserAndRolAsync(int id)
        {
            try
            {
                return await _context.roluser
                    .Include(ru => ru.User)
                    .Include(ru => ru.Rol)
                    .FirstOrDefaultAsync(ru => ru.id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener RolUser con ID {Id}", id);
                throw;
            }
        }



       

        public async Task<RolUser> CreateAsync(RolUser user)
        {
            try
            {
                await _context.Set<RolUser>().AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el rolusuario");
                throw;
            }
        }

      

        public async Task<bool> UpdateAsync(RolUser user)
        {
            try
            {
                _context.Set<RolUser>().Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el rolusuario");
                return false;
            }
        }


        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var rolUser = await _context.roluser.FindAsync(id);
                if (rolUser == null)
                    return false;

                _context.roluser.Remove(rolUser);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar RolUser");
                return false;
            }
        }

        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var rolUser = await GetByIdWithUserAndRolAsync(id);
                if (rolUser == null)
                    return false;

                rolUser.active = false;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar lógicamente RolUser con ID {Id}", id);
                return false;
            }
        }

        public async Task<bool> Recoverlogic(int id)
        {
            try
            {
                var rolUser = await GetByIdWithUserAndRolAsync(id);
                if (rolUser == null)
                {
                    return false;
                }

                rolUser.active = true;
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al recuperar la relacion: {ex.Message}");
                return false;
            }
        }



    }
}

