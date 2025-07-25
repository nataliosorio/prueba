using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Cita
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string MotivoConsulta { get; set; }

        // Relación con Paciente
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        // Relación con Doctor
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public bool Active { get; set; }
    }
}
