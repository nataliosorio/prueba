using Business.Interfaces;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Utilities.authorization;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    //[DynamicAuthorize]

    public class PizzasController : GenericController<PizzaDto>
    {
        public PizzasController(IGenericService<PizzaDto> service) : base(service)
        {
        }
    }
}
