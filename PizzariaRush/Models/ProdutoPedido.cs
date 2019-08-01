using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzariaRush.Models
{
    public class ProdutoPedido
    {
        public int Pedido { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public float ValorPedido { get; set; }
    }
}