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
    public class CustomerController: GenericController<CustomerDto>
    {
        public CustomerController(IGenericService<CustomerDto> service) : base(service)
        {

        }
    }
}
