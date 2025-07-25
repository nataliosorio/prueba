using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.Services;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business.Services
{
    public class UsersBusiness
    {
        private readonly UserData _userData;
        private readonly ILogger<UsersBusiness> _logger;

        public UsersBusiness(UserData userData, ILogger<UsersBusiness> logger)
        {
            _userData = userData;
            _logger = logger;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userData.GetAllWithPersonAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersDtoAsync()
        {
            try
            {
                var users = await _userData.GetAllWithPersonAsync(); // tu método actual

                return users.Select(u => new UserDto
                {
                    id = u.id,
                    username = u.username,
                    email = u.email,
                    password = u.password,
                    active = u.active,
                    personid = u.personid,
                    personname = u.Person?.firstname 
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al mapear usuarios a DTO");
                throw;
            }
        }



        //public async Task<User?> GetUserByIdAsync(int id)
        //{
        //    return await _userData.GetByIdWithPersonAsync(id);
        //}


        public async Task<UserDto?> GetByIdDtoAsync(int id)
        {
            var user = await _userData.GetByIdWithPersonAsync(id); // Asegúrate que incluya el .Include(u => u.Person)

            if (user == null)
                return null;

            return new UserDto
            {
                id = user.id,
                username = user.username,
                email = user.email,
                password = user.password,
                active = user.active,
                personid = user.personid,
                personname = user.Person?.firstname ?? ""
            };
        }

        //public async Task<User> CreateUserAsync(User user)
        //{
        //    return await _userData.CreateAsync(user);
        //}

        public async Task<User> CreateAsync(UserCreate dto)
        {
            var user = new User
            {
                username = dto.username,
                email = dto.email,
                password = dto.password,
                active = dto.active,
                personid = dto.personid
            };

            return await _userData.CreateAsync(user);
        }

       

        public async Task<bool> UpdateAsync(UserCreate dto)
        {
            var user = new User
            {
                id = dto.id,
                username = dto.username,
                email = dto.email,
                password = dto.password,
                active = dto.active,
                personid = dto.personid
            };

            return await _userData.UpdateAsync(user);
        }



        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userData.DeleteAsync(id);
        }

        public async Task<bool> DeleteUserLogicAsync(int id)
        {
            return await _userData.DeleteLogicAsync(id);
        }

        public async Task<bool> Recoverlogic(int id)
        {
           

            return await _userData.Recoverlogic(id);
         
        }

    }
}
