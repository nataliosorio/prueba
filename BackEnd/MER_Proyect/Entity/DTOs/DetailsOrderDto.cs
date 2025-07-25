using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class DetailsOrderDto
    {
        public int Id { get; set; }

        public int PedidoId { get; set; }

        public int PizzaId { get; set; }
        public string PizzaName { get; set; }   
        public decimal PizzaPrice { get; set; } 

        public int Cantidad { get; set; }
    }

}
