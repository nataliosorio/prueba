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
    public class RolUserBusiness
    {
        private readonly RolUserData _rolUserData;
        private readonly ILogger<RolUserBusiness> _logger;

        public RolUserBusiness(RolUserData rolUserData, ILogger<RolUserBusiness> logger)
        {
            _rolUserData = rolUserData;
            _logger = logger;
        }

        public async Task<IEnumerable<RolUser>> GetAllRolUsersAsync()
        {
            return await _rolUserData.GetAllWithUserAndRolAsync();
        }

        public async Task<IEnumerable<RolUserDto>> GetAllRolUsersDtoAsync()
        {
            try
            {
                var rolUsers = await _rolUserData.GetAllWithUserAndRolAsync();

                return rolUsers.Select(r => new RolUserDto
                {
                    id = r.id,
                    userid = r.userid,
                    username = r.User?.username ?? "",
                    rolid = r.rolid,
                    rolname = r.Rol?.name ?? "",
                    active = r.active
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al mapear RolUser a DTO");
                throw;
            }
        }

        public async Task<RolUserDto?> GetByIdDtoAsync(int id)
        {
            var rolUser = await _rolUserData.GetByIdWithUserAndRolAsync(id);

            if (rolUser == null)
                return null;

            return new RolUserDto
            {
                id = rolUser.id,
                userid = rolUser.userid,
                username = rolUser.User?.username ?? "",
                rolid = rolUser.rolid,
                rolname = rolUser.Rol?.name ?? "",
                active = rolUser.active
            };
        }

      
        public async Task<RolUser> CreateAsync(RolUserCreate dto)
        {
            var user = new RolUser
            {
                userid = dto.userid,
                rolid = dto.rolid,
                active = dto.active
            };

            return await _rolUserData.CreateAsync(user);
        }

       

        public async Task<bool> UpdateAsync(RolUserCreate dto)
        {
            var user = new RolUser
            {
                id = dto.id,
                userid = dto.userid,
                rolid = dto.rolid,
                active = dto.active
            };

            return await _rolUserData.UpdateAsync(user);
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


            return await _rolUserData.Recoverlogic(id);

        }
    }
}
