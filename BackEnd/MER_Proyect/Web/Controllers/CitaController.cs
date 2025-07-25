using Business.Services;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.authorization;
using Utilities.Exceptions;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    //[DynamicAuthorize]

    [ApiController]
    [Produces("application/json")]
    public class CitaController: ControllerBase
    {
        private readonly CitaBusiness _citaBusiness;
        private readonly ILogger<CitaController> _logger;

        public CitaController(CitaBusiness citaBusiness, ILogger<CitaController> logger)
        {
            _citaBusiness = citaBusiness;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitaDto>>> GetAll()
        {
            var citas = await _citaBusiness.GetAllCitasDtoAsync();
            return Ok(citas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var citaDto = await _citaBusiness.GetByIdDtoAsync(id);
            if (citaDto == null)
                return NotFound();

            return Ok(citaDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CitaCreate dto)
        {
            try
            {
                var createdCita = await _citaBusiness.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdCita.Id }, createdCita);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la cita");
                return StatusCode(500, $"Error al crear la cita: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CitaCreate dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID de la URL no coincide con el del cuerpo.");

            var success = await _citaBusiness.UpdateAsync(dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _citaBusiness.DeleteCitaAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("DeleteLogic/{id}")]
        public async Task<IActionResult> DeleteLogic(int id)
        {
            var result = await _citaBusiness.DeleteCitaLogicAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("restore/{id}")]
        public virtual async Task<IActionResult> RecoverLogic(int id)
        {
            try
            {
                var result = await _citaBusiness.RecoverLogicAsync(id);
                if (!result)
                    throw new NotFoundControllerException($"No se encontró la cita con ID {id} para restaurar.");

                return Ok();
            }
            catch (ControllerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorControllerException($"Error al restaurar la cita con ID {id}.", ex);
            }
        }
    }
}
