using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzariaRush.DAL;
using PizzariaRush.Models;

namespace PizzariaRush.Controllers
{
    public class OperacaoController : Controller
    {
        // GET: Operacao
        public ActionResult Pedido()
        {
            ViewBag.Produtos = ProdutoDAL.ListarProdutos();
            ViewBag.Clientes = ClienteDAL.ListarClientes();
            ViewBag.Pedidos = PedidoDAL.ListarPedidos();
            foreach (var pedido in ViewBag.Pedidos)
            {
                pedido.listaProdutosPedidos = ProdutoPedidoDAL.ListarProdutosPedidos(pedido.Id);
            }
            return View();
        }
        public ActionResult ObterPedido(int id)
        {
            return Json(PedidoDAL.ListarPedidos().Find(x => x.Id == id));
        }

        public ActionResult ExcluirPedido(int id)
        {
            var ret = false;
            var pedido = PedidoDAL.ListarPedidos().Find(x => x.Id == id);
            if (pedido != null)
            {
                PedidoDAL.ListarPedidos().Remove(pedido);
                PedidoDAL.RemoverPedido(pedido.Id);
                ret = true;
            }

            return Json(ret);
        }

        public ActionResult SalvarPedido(Pedido pedido)
        {
           //float valorTotal = 0;
            var pedidoBD = PedidoDAL.ListarPedidos().Find(x => x.Id == pedido.Id);
            if (pedidoBD == null)
            {
                pedidoBD = pedido;
                pedidoBD.Id = PedidoDAL.GetMaxId() + 1;
                foreach (var produtopedido in pedido.listaProdutosPedidos)
                { 
                    produtopedido.Pedido = pedidoBD.Id;
                }
            }
            else
            {
                pedidoBD.Cliente = pedido.Cliente;
                pedidoBD.ValorTotal = pedido.ValorTotal;
                pedidoBD.listaProdutosPedidos = pedido.listaProdutosPedidos;
                ProdutoPedidoDAL.RemoverProdutoPedido(pedidoBD);
            }

            PedidoDAL.IncluirPedido(pedidoBD);
            return Json(pedidoBD);
        }
    }
}