using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzariaRush.Models;
using PizzariaRush.DAL;

namespace PizzariaRush.Controllers
{
    public class CadastroController : Controller
    {
        // GET: Cadastro

        #region Categoria
        public ActionResult Categoria()
        {
            ViewBag.Categoria = CategoriaDAL.ListarCategorias();
            return View();
        }

        public ActionResult ObterCategoria(int id)
        {
            return Json(CategoriaDAL.ListarCategorias().Find(x => x.Id == id));
        }

        public ActionResult ExcluirCategoria(int id)
        {
            var ret = false;
            var categoria = CategoriaDAL.ListarCategorias().Find(x => x.Id == id);

            if (categoria != null)
            {
                CategoriaDAL.ListarCategorias().Remove(categoria);
                CategoriaDAL.RemoverCategoria(categoria);
                ret = true;
            }

            return Json(ret);
        }

        public ActionResult SalvarCategoria(Categoria categoria)
        {
            var categoriaBD = CategoriaDAL.ListarCategorias().Find(x => x.Id == categoria.Id);

            if (categoriaBD == null)
            {
                categoriaBD = categoria;
                categoriaBD.Id = CategoriaDAL.GetMaxId() + 1;
            }
            else
                categoriaBD.Nome = categoria.Nome;

            CategoriaDAL.IncluirCategoria(categoriaBD);
            return Json(categoriaBD);
        }
        #endregion

        #region Produto
        public ActionResult Produto()
        {
            ViewBag.Produto = ProdutoDAL.ListarProdutos();
            ViewBag.Categoria = CategoriaDAL.ListarCategorias();
            return View();
        }

        public ActionResult ObterProduto(int id)
        {
            return Json(ProdutoDAL.ListarProdutos().Find(x => x.Id == id));
        }

        public ActionResult ExcluirProduto(int id)
        {
            var ret = false;
            var produto = ProdutoDAL.ListarProdutos().Find(x => x.Id == id);

            if (produto != null)
            {
                ProdutoDAL.ListarProdutos().Remove(produto);
                ProdutoDAL.RemoverProduto(produto);
                ret = true;
            }

            return Json(ret);
        }

        public ActionResult SalvarProduto(Produto produto)
        {
            var produtoBD = ProdutoDAL.ListarProdutos().Find(x => x.Id == produto.Id);

            if (produtoBD == null)
            {
                produtoBD = produto;
                produtoBD.Id = ProdutoDAL.GetMaxId() + 1;
            }
            else
            {
                produtoBD.Categoria.Id = produto.Categoria.Id;
                produtoBD.Categoria.Nome = produto.Categoria.Nome;
                produtoBD.Nome = produto.Nome;
                produtoBD.Valor = produto.Valor;
            }

            ProdutoDAL.IncluirProduto(produtoBD);
            return Json(produtoBD);

        }
        #endregion

        #region Cliente
        public ActionResult Cliente()
        {
            ViewBag.Cliente = ClienteDAL.ListarClientes();
            return View();
        }

        public ActionResult ObterCliente(int id)
        {
            return Json(ClienteDAL.ListarClientes().Find(x => x.Id == id));
        }

        public ActionResult ExcluirCliente(int id)
        {
            var ret = false;
            var cliente = ClienteDAL.ListarClientes().Find(x => x.Id == id);

            if (cliente != null)
            {
                ClienteDAL.ListarClientes().Remove(cliente);
                ClienteDAL.RemoverCliente(cliente);
                ret = true;
            }

            return Json(ret);
        }

        public ActionResult SalvarCliente(Cliente cliente)
        {
            var clienteBD = ClienteDAL.ListarClientes().Find(x => x.Id == cliente.Id);

            if (clienteBD == null)
            {
                clienteBD = cliente;
                clienteBD.Id = ClienteDAL.GetMaxId() + 1;
            }
            else
            {
                clienteBD.Nome = cliente.Nome;
                clienteBD.Telefone = cliente.Telefone;
                clienteBD.Endereco = cliente.Endereco;
                clienteBD.Numero = cliente.Numero;
                clienteBD.Complemento = cliente.Complemento;
                clienteBD.Bairro = cliente.Bairro;
                clienteBD.Cidade = cliente.Cidade;
            }
            ClienteDAL.IncluirCliente(clienteBD);
            return Json(clienteBD);
        }
        public ActionResult ChecarCliente(string telefone)
        {
            return Json(ClienteDAL.ListarClientes().Find(x => x.Telefone == telefone));
        }
        #endregion

    }
}