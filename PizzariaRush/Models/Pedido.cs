using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzariaRush.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public float ValorTotal { get; set; }
        public List<ProdutoPedido> listaProdutosPedidos { get; set; }
    }
}