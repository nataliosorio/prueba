//using Business.Interfaces;
//using Business.Services;
//using Data.Services;
//using Entity.DTOs;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Utilities.authorization;
//using Utilities.Exceptions;

//namespace Web.Controllers
//{

//    [Route("api/[controller]")]
//    //[Authorize]
//    //[DynamicAuthorize]

//    [ApiController]
//    [Produces("application/json")]

//    public class RolFormPermissionController : ControllerBase
//    {

//        private readonly RolFormPermissionBusiness _rolUserBusiness;
//        private readonly ILogger<RolFormPermissionController> _logger;

//        public RolFormPermissionController(RolFormPermissionBusiness rolUserBusiness, ILogger<RolFormPermissionController> logger)
//        {
//            _rolUserBusiness = rolUserBusiness;
//            _logger = logger;
//        }


//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<RolFormPermissionDto>>> GetAll()
//        {
//            var rolUsers = await _rolUserBusiness.GetAllRolFormPermissionsDtoAsync();
//            return Ok(rolUsers);
//        }



//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var rolUserDto = await _rolUserBusiness.GetByIdDtoAsync(id);

//            if (rolUserDto == null)
//                return NotFound();

//            return Ok(rolUserDto);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] rolFormPermissionCreate dto)
//        {
//            try
//            {
//                var createdUser = await _rolUserBusiness.CreateAsync(dto);
//                return CreatedAtAction(nameof(GetById), new { id = createdUser.id }, createdUser);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Error al crear el Rolusuario: {ex.Message}");
//            }
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> Update(int id, [FromBody] rolFormPermissionCreate dto)
//        {
//            if (id != dto.id)
//                return BadRequest("El ID de la URL no coincide con el del cuerpo.");

//            var success = await _rolUserBusiness.UpdateAsync(dto);
//            if (!success)
//                return NotFound();

//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var result = await _rolUserBusiness.DeleteRolUserAsync(id);
//            if (!result)
//                return NotFound();

//            return NoContent();
//        }

//        [HttpDelete("DeleteLogic/{id}")]
//        public async Task<IActionResult> DeleteLogic(int id)
//        {
//            var result = await _rolUserBusiness.DeleteRolUserLogicAsync(id);
//            if (!result)
//                return NotFound();

//            return NoContent();
//        }

//        [HttpPatch("restore/{id}")]
//        public virtual async Task<IActionResult> RecoverLogic(int id)
//        {
//            try
//            {
//                var result = await _rolUserBusiness.Recoverlogic(id);
//                if (!result)
//                    throw new NotFoundControllerException($"No se encontró el recurso con ID {id} para restaurar.");

//                return Ok();
//            }
//            catch (ControllerException)
//            {
//                throw;
//            }
//            catch (Exception ex)
//            {
//                throw new InternalServerErrorControllerException($"Error al restaurar el recurso con ID {id}.", ex);
//            }
//        }


//    }
//}
