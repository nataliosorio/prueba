using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class CreateOrderDto
    {
        public int ClienteId { get; set; }
        public int PizzaId { get; set; }
        public int Cantidad { get; set; }

        public string Estado { get; set; } // "Pendiente", "Entregado", etc.

    }

}
