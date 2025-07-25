using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class RolUserDto
    {
        public int id { get; set; }
        public int userid { get; set; }
        public string username { get; set; }
        public int rolid { get; set; }
        public string rolname { get; set; }
        public bool active { get; set; }


    }


}
