using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class RegisterDto
    {
        // Datos de la persona
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phonenumber { get; set; }

        // Datos del usuario
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
