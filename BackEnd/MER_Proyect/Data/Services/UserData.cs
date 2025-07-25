using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context;
using Entity.DTOs;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Services
{
    public class UserData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserData> _logger;

        public UserData(ApplicationDbContext context, ILogger<UserData> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<User>> GetAllWithPersonAsync()
        {
            try
            {
                return await _context.User
                    .Include(u => u.Person) // Hace el JOIN con la tabla Person
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las citas con información de la persona");
                throw;
            }
        }

        public async Task<User?> GetByIdWithPersonAsync(int id)
        {
            try
            {
                return await _context.User
                    .Include(u => u.Person) // Hace el INNER JOIN con la tabla Person
                    .FirstOrDefaultAsync(u => u.id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las citas con ID {Id}", id);
                throw;
            }
        }

        public async Task<User> CreateAsync(User user)
        {
            try
            {
                await _context.Set<User>().AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el la citas ");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(User user)
        {
            try
            {
                _context.Set<User>().Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la citas ");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var user = await _context.Set<User>().FindAsync(id);
                if (user == null)
                    return false;
                _context.Set<User>().Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la citas ");
                return false;
            }
        }


        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var Users = await GetByIdWithPersonAsync(id);
                if (Users == null)
                {
                    return false;
                }

                Users.active = false;
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar de manera Users el Rol: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Recoverlogic(int id)
        {
            try
            {
                var Users = await GetByIdWithPersonAsync(id);
                if (Users == null)
                {
                    return false;
                }

                Users.active = true;
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al recuperar el usuario: {ex.Message}");
                return false;
            }
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            try
            {
                return await _context.User
                    .FirstOrDefaultAsync(u => u.email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar el usuario por email");
                throw;
            }
        }



    }
}
