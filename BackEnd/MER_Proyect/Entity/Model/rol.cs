using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class rol
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool active { get; set; }

        public ICollection<RolUser> RolUser { get; set; }

        public ICollection<RolFormPermission> RolFormPermission { get; set; }
    }
}
