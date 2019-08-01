using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzariaRush.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public Categoria Categoria { get; set; }
        public string Nome { get; set; }
        public float Valor { get; set; }
    }
}