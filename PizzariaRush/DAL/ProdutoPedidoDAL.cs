using PizzariaRush.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzariaRush.DAL
{
    public class ProdutoPedidoDAL
    {
        public static List<ProdutoPedido> listaProdutosPedidos;

        public static List<ProdutoPedido> ListarProdutosPedidos(int id)
        {
            string select = "select pp.ID_PEDIDO, pr.ID_PRODUTO, c.ID_CAT, c.NM_CAT, pr.NM_PRODUTO, pr.VAL_PRODUTO, pp.QTD_PRODUTO, pp.VAL_PEDIDO from PRODUTOSPEDIDO AS pp " +
                "JOIN PRODUTOS AS pr " +
                "JOIN CATEGORIAS AS c " +
                "ON pp.ID_PRODUTO = pr.ID_PRODUTO AND pr.ID_CAT = c.ID_CAT " +
                $"WHERE pp.ID_PEDIDO = {id};";

            Conexao.ExecuteReader(Conexao.Command(select));

            listaProdutosPedidos = new List<ProdutoPedido>();

            while (Conexao.dataReader.Read())
            {
                ProdutoPedido produtoPedido = new ProdutoPedido
                {
                    Pedido = (int)Conexao.dataReader["ID_PEDIDO"],
                    Produto = new Produto
                    {
                        Id = (int)Conexao.dataReader["ID_PRODUTO"],
                        Categoria = new Categoria
                        {
                            Id = (int)Conexao.dataReader["ID_CAT"],
                            Nome = (string)Conexao.dataReader["NM_CAT"]
                        },
                        Nome = (string)Conexao.dataReader["NM_PRODUTO"],
                        Valor = float.Parse(Conexao.dataReader["VAL_PRODUTO"].ToString())
                    },
                    Quantidade = (int)Conexao.dataReader["QTD_PRODUTO"],
                    ValorPedido = float.Parse(Conexao.dataReader["VAL_PEDIDO"].ToString()),
                };
                listaProdutosPedidos.Add(produtoPedido);
            }
            return listaProdutosPedidos;
        }

        public static void RemoverProdutoPedido(Pedido pedido)
        {
            string deleteProdPed = $"DELETE FROM PRODUTOSPEDIDO WHERE ID_PEDIDO = {pedido.Id}";

            Conexao.ExecuteNonQuery(Conexao.Command(deleteProdPed));
        }

    }
}