using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Usuario { get; set; }
        public List<string> Roles { get; set; }
    }
}
