using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class OrdersDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; }

        public int PizzaId { get; set; }
        public string PizzaNombre { get; set; }
        public decimal PizzaPrecio { get; set; }

        public int Cantidad { get; set; }

        public string Estado { get; set; }
    }


}
