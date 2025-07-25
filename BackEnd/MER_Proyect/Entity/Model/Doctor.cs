using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Doctor
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Especialidad { get; set; }
        public string NumeroDeColegiado { get; set; }

        // Relación: Un doctor puede tener muchas citas
        public ICollection<Cita> Citas { get; set; }
    }
}
