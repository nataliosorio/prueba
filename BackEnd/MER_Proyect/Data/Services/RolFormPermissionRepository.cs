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
using Utilities.Aud.CurrentUser;
using Utilities.Aud.Services;

namespace Data.Services
{
    public class RolFormPermissionRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<RolFormPermissionRepository> _logger;
        public RolFormPermissionRepository(ApplicationDbContext context, ILogger<RolFormPermissionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<RolFormPermissionDto>> GetAllJoinAsync()
        {
            return await _context.rolformpermission
                .Include(x => x.Rol)
                .Include(x => x.Form)
                .Include(x => x.Permission)
                .Select(rfp => new RolFormPermissionDto
                {
                    id = rfp.id,
                    rolid = rfp.rolid,
                    formid = rfp.formid,
                    permissionid = rfp.permissionid,
                    rolname = rfp.Rol.name,
                    formname = rfp.Form.name,
                    permissionname = rfp.Permission.name,

                })
                .ToListAsync();
        }

        public async Task<IEnumerable<RolFormPermission>> GetAllWithRolFormPermissionAsync()
        {
            try
            {
                return await _context.rolformpermission
                    .Include(rfp => rfp.Rol)
                    .Include(rfp => rfp.Form)
                    .Include(rfp => rfp.Permission)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los registros de RolFormPermission con relaciones");
                throw;
            }
        }


        public async Task<RolFormPermission?> GetByIdWithRolFormPermissionAsync(int id)
        {
            try
            {
                return await _context.rolformpermission
                    .Include(rfp => rfp.Rol)
                    .Include(rfp => rfp.Form)
                    .Include(rfp => rfp.Permission)
                    .FirstOrDefaultAsync(rfp => rfp.id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener RolFormPermission con ID {Id}", id);
                throw;
            }
        }

        public async Task<RolFormPermission> CreateAsync(RolFormPermission rolFormPermission)
        {
            try
            {
                await _context.Set<RolFormPermission>().AddAsync(rolFormPermission);
                await _context.SaveChangesAsync();
                return rolFormPermission;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el RolFormPermission");
                throw;
            }
        }



        public async Task<bool> UpdateAsync(RolFormPermission rolFormPermission)
        {
            try
            {
                _context.Set<RolFormPermission>().Update(rolFormPermission);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el RolFormPermission");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var rolFormPermission = await _context.rolformpermission.FindAsync(id);
                if (rolFormPermission == null)
                    return false;

                _context.rolformpermission.Remove(rolFormPermission);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar RolFormPermission");
                return false;
            }
        }

        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var rolFormPermission = await GetByIdWithRolFormPermissionAsync(id);
                if (rolFormPermission == null)
                    return false;

                rolFormPermission.active = false;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar lógicamente RolFormPermission con ID {Id}", id);
                return false;
            }
        }

        public async Task<bool> RecoverLogicAsync(int id)
        {
            try
            {
                var rolFormPermission = await GetByIdWithRolFormPermissionAsync(id);
                if (rolFormPermission == null)
                {
                    return false;
                }

                rolFormPermission.active = true;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al recuperar la relación RolFormPermission con ID {id}: {ex.Message}");
                return false;
            }
        }








    }
}
