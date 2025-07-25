using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Orders
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public int ClienteId { get; set; }
        public Customer Customer { get; set; }

        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }

        public int Cantidad { get; set; }

        public string Estado { get; set; } // "Pendiente", "Entregado", etc.
    }


}
