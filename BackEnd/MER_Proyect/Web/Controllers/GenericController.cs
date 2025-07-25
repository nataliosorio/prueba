

using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.authorization;
using Utilities.Exceptions;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class GenericController<TDto> : ControllerBase where TDto : class
    {
        private readonly IGenericService<TDto> _service;

        public GenericController(IGenericService<TDto> service)
        {
            _service = service;
        }


        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorControllerException("Ocurrió un error al obtener todos los registros.", ex);
            }
        }


        [HttpGet("{id}")]


        public virtual async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                    throw new NotFoundControllerException($"No se encontró el recurso con ID {id}.");

                return Ok(result);
            }
            catch (ControllerException)
            {
                throw; // Re-lanza si ya es una excepción controlada
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorControllerException($"Error al obtener el recurso con ID {id}.", ex);
            }
        }


        [HttpPost]

        public virtual async Task<IActionResult> Create([FromBody] TDto dto)
        {
            try
            {
                if (dto == null)
                    throw new BadRequestControllerException("El objeto enviado es nulo.");

                var result = await _service.CreateAsync(dto);
                return Ok(result);
            }
            catch (ControllerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorControllerException("Error al crear el recurso.", ex);
            }
        }


        [HttpPut("{id}")]


        public virtual async Task<IActionResult> Update(int id, [FromBody] TDto dto)
        {
            try
            {
                var result = await _service.UpdateAsync(dto);
                return Ok(result);
            }
            catch (ControllerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorControllerException($"Error al actualizar el recurso con ID {id}.", ex);
            }
        }


        [HttpDelete("{id}")]


        public virtual async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                return Ok(result);
            }
            catch (ControllerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorControllerException($"Error al eliminar el recurso con ID {id}.", ex);
            }
        }



        [HttpDelete("logic/{id}")]


        public virtual async Task<IActionResult> DeleteLogic(int id)
        {
            try
            {
                var result = await _service.DeleteAsyncLogic(id);
                if (!result)
                    throw new NotFoundControllerException($"No se encontró el recurso lógico con ID {id} para eliminar.");

                return Ok();
            }
            catch (ControllerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorControllerException($"Error al eliminar lógicamente el recurso con ID {id}.", ex);
            }
        }

        [HttpPatch("restore/{id}")]



        public virtual async Task<IActionResult> RecoverLogic(int id)
        {
            try
            {
                var result = await _service.Recoverlogic(id);
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
