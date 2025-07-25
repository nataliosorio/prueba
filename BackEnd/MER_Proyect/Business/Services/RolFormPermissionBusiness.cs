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
    public class RolFormPermissionBusiness
    {

        private readonly RolFormPermissionRepository _rolUserData;
        private readonly ILogger<RolFormPermissionBusiness> _logger;

        public RolFormPermissionBusiness(RolFormPermissionRepository rolFormPermissionRepository, ILogger<RolFormPermissionBusiness> logger)
        {
            _rolUserData = rolFormPermissionRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<RolFormPermissionDto>> GetAllRolFormPermissionsDtoAsync()
        {
            try
            {
                var rolFormPermissions = await _rolUserData.GetAllWithRolFormPermissionAsync();

                return rolFormPermissions.Select(r => new RolFormPermissionDto
                {
                    id = r.id,
                    rolid = r.rolid,
                    rolname = r.Rol?.name ?? "",
                    formid = r.formid,
                    formname = r.Form?.name ?? "",
                    permissionid = r.permissionid,
                    permissionname = r.Permission?.name ?? "",
                    active = r.active
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al mapear RolFormPermission a DTO");
                throw;
            }
        }

        public async Task<RolFormPermissionDto?> GetByIdDtoAsync(int id)
        {
            var rolFormPermission = await _rolUserData.GetByIdWithRolFormPermissionAsync(id);

            if (rolFormPermission == null)
                return null;

            return new RolFormPermissionDto
            {
                id = rolFormPermission.id,
                rolid = rolFormPermission.rolid,
                rolname = rolFormPermission.Rol?.name ?? "",
                formid = rolFormPermission.formid,
                formname = rolFormPermission.Form?.name ?? "",
                permissionid = rolFormPermission.permissionid,
                permissionname = rolFormPermission.Permission?.name ?? "",
                active = rolFormPermission.active
            };
        }

        public async Task<RolFormPermission> CreateAsync(rolFormPermissionCreate dto)
        {
            var rolFormPermission = new RolFormPermission
            {
                rolid = dto.rolid,
                formid = dto.formid,
                permissionid = dto.permissionid,
                active = dto.active
            };

            return await _rolUserData.CreateAsync(rolFormPermission);
        }

        public async Task<bool> UpdateAsync(rolFormPermissionCreate dto)
        {
            var rolFormPermission = new RolFormPermission
            {
                id = dto.id,
                rolid = dto.rolid,
                formid = dto.formid,
                permissionid = dto.permissionid,
                active = dto.active
            };

            return await _rolUserData.UpdateAsync(rolFormPermission);
        }


        public async Task<bool> DeleteRolUserAsync(int id)
        {
            return await _rolUserData.DeleteAsync(id);
        }

        public async Task<bool> DeleteRolUserLogicAsync(int id)
        {
            return await _rolUserData.DeleteLogicAsync(id);
        }

        public async Task<bool> Recoverlogic(int id)
        {


            return await _rolUserData.RecoverLogicAsync(id);

        }








    }
}
