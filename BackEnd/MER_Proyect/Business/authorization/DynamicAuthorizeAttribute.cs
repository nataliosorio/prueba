using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Utilities.authorization
{
    public class DynamicAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

           

            var roles = context.HttpContext.User.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();
          
            Console.WriteLine($"Roles en token: {string.Join(", ", roles)}");

            // Obtener el nombre del controlador y acción
            var controller = context.RouteData.Values["controller"]?.ToString();
            Console.WriteLine($"Nombre del controlador: {controller }");
            var action = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;

            // Acceder directamente al repositorio desde el servicio
            var repository = context.HttpContext.RequestServices.GetService<RolFormPermissionRepository>();

            if (repository == null)
            {
                context.Result = new StatusCodeResult(500); // Error interno si no se puede inyectar
                return;
            }

            // Obtener permisos desde la BD
            var permisos = repository.GetAllJoinAsync().Result; // Ojo, si quieres evitar .Result, convierte el método en async

            // Validar si alguno de los roles del usuario tiene permiso sobre ese form y acción
            var tienePermiso = permisos.Any(p =>
                p.formname.Equals(controller, StringComparison.OrdinalIgnoreCase) &&
                p.permissionname.Equals(action, StringComparison.OrdinalIgnoreCase) &&
                roles.Contains(p.rolname));

            if (!tienePermiso)
            {
                context.Result = new ForbidResult(); // 403 - Prohibido
            }
        }
    }
}