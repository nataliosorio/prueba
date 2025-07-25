using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class rolFormPermissionCreate
    {
        public int id { get; set; }
        public int rolid { get; set; }
        public int formid { get; set; }
        public int permissionid { get; set; }
        public bool active { get; set; }
    }
}
