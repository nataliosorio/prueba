﻿using Business.Interfaces;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.authorization;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]

    [DynamicAuthorize]

    public class PersonController : GenericController<PersonDto>
    {
        public PersonController(IGenericService<PersonDto> service) : base(service)
        {
        }
    }
}
