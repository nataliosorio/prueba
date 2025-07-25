using Business.Interfaces;
using Business.Services;
using Data.Services;
using Entity.DTOs;
using Entity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utilities.authorization;
using Utilities.Exceptions;
using Web.Controllers;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [DynamicAuthorize]

    [ApiController]
    [Produces("application/json")]

    public class UserController : ControllerBase
    {
        private readonly UsersBusiness _userBusiness;
        private readonly ILogger<UserController> _logger;

        public UserController(UsersBusiness usersBusiness, ILogger<UserController> logger)
        {
            _userBusiness = usersBusiness;
            _logger = logger;
        }


        [HttpGet]
        //public async Task<ActionResult<IEnumerable<User>>> GetAll()
        //{
        //    var users = await _userBusiness.GetAllUsersDtoAsync();
        //    return Ok(users);
        //}

        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            try
            {
                var users = await _userBusiness.GetAllUsersDtoAsync();
                return Ok(users);

            } catch (Exception ex)
            {
                throw new InternalServerErrorControllerException("Error al obtener todos los usuarios.", ex);
            }
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var userDto = await _userBusiness.GetByIdDtoAsync(id);

                if (userDto == null)
                    throw new NotFoundControllerException($"Usuario con ID {id} no encontrado.");

                return Ok(userDto);
            }
            catch (ControllerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorControllerException($"Error al obtener el usuario con ID {id}.", ex);
            }
        }

        //public async Task<IActionResult> GetById(int id)
        //{
        //    var userDto = await _userBusiness.GetByIdDtoAsync(id);

        //    if (userDto == null)
        //        return NotFound();

        //    return Ok(userDto);
        //}



        [HttpPost]

        public async Task<IActionResult> Create([FromBody] UserCreate dto)
        {
            try
            {
                if (dto == null)
                    throw new BadRequestControllerException("El objeto del usuario está vacío o mal formado.");

                var createdUser = await _userBusiness.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdUser.id }, createdUser);
            }
            catch (ControllerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorControllerException("Error al crear el usuario.", ex);
            }
        }

        //public async Task<IActionResult> CreateUser([FromBody] UserCreate dto)
        //{
        //    try
        //    {
        //        var createdUser = await _userBusiness.CreateAsync(dto);
        //        return CreatedAtAction(nameof(GetById), new { id = createdUser.id }, createdUser);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al crear el usuario: {ex.Message}");
        //    }
        //}



        [HttpPut("{id}")]


        public async Task<IActionResult> Update(int id, [FromBody] UserCreate dto)
        {
            try
            {
                if (id != dto.id)
                    throw new BadRequestControllerException("El ID de la URL no coincide con el del cuerpo de la solicitud.");

                var success = await _userBusiness.UpdateAsync(dto);

                if (!success)
                    throw new NotFoundControllerException($"No se encontró el usuario con ID {id} para actualizar.");

                return NoContent();
            }
            catch (ControllerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorControllerException($"Error al actualizar el usuario con ID {id}.", ex);
            }
        }

        //public async Task<IActionResult> Update(int id, [FromBody] UserCreate dto)
        //{
        //    if (id != dto.id)
        //        return BadRequest("El ID de la URL no coincide con el del cuerpo.");

        //    var success = await _userBusiness.UpdateAsync(dto);
        //    if (!success)
        //        return NotFound();

        //    return NoContent();
        //}

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _userBusiness.DeleteUserAsync(id);
                if (!result)
                    throw new NotFoundControllerException($"No se encontró el usuario con ID {id} para eliminar.");

                return NoContent();
            }
            catch (ControllerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorControllerException($"Error al eliminar el usuario con ID {id}.", ex);
            }
        }

        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _userBusiness.DeleteUserAsync(id);
        //    if (!result)
        //        return NotFound();

        //    return NoContent();
        //}

        [HttpDelete("logic/{id}")]

        public async Task<IActionResult> DeleteLogic(int id)
        {
            try
            {
                var result = await _userBusiness.DeleteUserLogicAsync(id);
                if (!result)
                    throw new NotFoundControllerException($"No se encontró el usuario con ID {id} para eliminación lógica.");

                return NoContent();
            }
            catch (ControllerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorControllerException($"Error al eliminar lógicamente el usuario con ID {id}.", ex);
            }
        }
    


        //public async Task<IActionResult> DeleteLogic(int id)
        //{
        //    var result = await _userBusiness.DeleteUserLogicAsync(id);
        //    if (!result)
        //        return NotFound();

        //    return NoContent();
        //}

        [HttpPatch("restore/{id}")]
        public virtual async Task<IActionResult> RecoverLogic(int id)
        {
            try
            {
                var result = await _userBusiness.Recoverlogic(id);
                if (!result)
                    throw new NotFoundControllerException($"No se encontró el recurso con ID {id} para restaurar.");

                return Ok();
            }
            catch (ControllerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorControllerException($"Error al restaurar el recurso con ID {id}.", ex);
            }
        }



    }

}




