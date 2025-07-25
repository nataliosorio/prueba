using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class CitaDto
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string MotivoConsulta { get; set; }

        public int PacienteId { get; set; }
        public string NombrePaciente { get; set; }

        public int DoctorId { get; set; }
        public string NombreDoctor { get; set; }
        public bool Active { get; set; }

    }
}
