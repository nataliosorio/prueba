using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class RolUserCreate
    {
        public int id { get; set; }
        public int rolid { get; set; }
        public int userid { get; set; }
        public bool active { get; set; }
    }
}
