using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class DetailsOrder
    {
        public int Id { get; set; }

        public int PedidoId { get; set; }
        public Orders Orders { get; set; }

        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }

        public int Cantidad { get; set; }
    }

}
