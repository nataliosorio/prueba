using Business.Interfaces;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.authorization;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [DynamicAuthorize]

    public class RolController : GenericController<rolDto>
    {
        public RolController(IGenericService<rolDto> service) : base(service)
        {
        }
    }
}
