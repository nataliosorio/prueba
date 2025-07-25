using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Entity.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Business.Services
{
    public class JwtService 
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerarToken(User usuario, List<string> roles)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            // Tiempo de expiración general del token (obligatorio)
            var expirationMinutes = Convert.ToDouble(_configuration["JWT:DurationInMinutes"]);
            var expirationDate = DateTime.UtcNow.AddMinutes(expirationMinutes);

            // Tiempo de inactividad permitido (este será un claim personalizado)
            var inactivityMinutes = _configuration["JWT:IdleTimeoutInMinutes"];

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, usuario.id.ToString()),
        new Claim(ClaimTypes.Name, usuario.username),
        new Claim("inactividad", inactivityMinutes) 
    };

            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
