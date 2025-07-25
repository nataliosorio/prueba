using Business.Services;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Exceptions;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    //[DynamicAuthorize]
    [ApiController]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersBusiness _ordersBusiness;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(OrdersBusiness ordersBusiness, ILogger<OrdersController> logger)
        {
            _ordersBusiness = ordersBusiness;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersDto>>> GetAll()
        {
            var orders = await _ordersBusiness.GetAllDtoAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var orderDto = await _ordersBusiness.GetByIdDtoAsync(id);
            if (orderDto == null)
                return NotFound();

            return Ok(orderDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            try
            {
                var createdOrder = await _ordersBusiness.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdOrder.Id }, createdOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el pedido");
                return StatusCode(500, $"Error al crear el pedido: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrdersDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID de la URL no coincide con el del cuerpo.");

            var success = await _ordersBusiness.UpdateAsync(dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ordersBusiness.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("DeleteLogic/{id}")]
        public async Task<IActionResult> DeleteLogic(int id)
        {
            var result = await _ordersBusiness.DeleteLogicAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("restore/{id}")]
        public virtual async Task<IActionResult> RecoverLogic(int id)
        {
            try
            {
                var result = await _ordersBusiness.RecoverLogicAsync(id);
                if (!result)
                    throw new NotFoundControllerException($"No se encontró el pedido con ID {id} para restaurar.");

                return Ok();
            }
            catch (ControllerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorControllerException($"Error al restaurar el pedido con ID {id}.", ex);
            }
        }
    }
}
