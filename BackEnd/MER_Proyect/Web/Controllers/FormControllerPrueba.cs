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

    public class FormControllerPrueba : GenericController<FormDto>
    {
        public FormControllerPrueba(IGenericService<FormDto> service) : base(service)
        {
        }
    }
}
