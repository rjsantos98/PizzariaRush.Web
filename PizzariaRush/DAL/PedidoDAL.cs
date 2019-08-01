using PizzariaRush.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzariaRush.DAL
{
    public class PedidoDAL
    {
        public static List<Pedido> listaPedidos;

        public static List<Pedido> ListarPedidos()
        {
            string select = "SELECT"
                    + " p.ID_PEDIDO"
                    + " ,c.ID_CLIENTE as id_cliente"
                    + " ,c.NM_CLIENTE as NM_CLIENTE"
                    + " ,c.TEL_CLIENTE as TEL_CLIENTE"
                    + " ,c.END_CLIENTE as END_CLIENTE"
                    + " ,c.NUM_CLIENTE as NUM_CLIENTE"
                    + " ,c.COM_CLIENTE as COM_CLIENTE"
                    + " ,c.BAO_CLIENTE as BAO_CLIENTE"
                    + " ,c.CID_CLIENTE as CID_CLIENTE"
                    + " ,p.VAL_TOTAL as VAL_TOTAL"
                    + " FROM PEDIDOS as p " 
                    + " JOIN CLIENTES as c"
                    + " ON p.ID_CLIENTE= c.ID_CLIENTE;";

            Conexao.ExecuteReader(Conexao.Command(select));

            listaPedidos = new List<Pedido>();

            while(Conexao.dataReader.Read())
            {
                Pedido pedido = new Pedido
                {
                    Id = (int)Conexao.dataReader["ID_PEDIDO"],
                    Cliente = new Cliente
                    {
                        Id = (int)Conexao.dataReader["ID_CLIENTE"],
                        Nome = (string)Conexao.dataReader["NM_CLIENTE"],
                        Telefone = (string)Conexao.dataReader["TEL_CLIENTE"],
                        Endereco = (string)Conexao.dataReader["END_CLIENTE"],
                        Numero = (string)Conexao.dataReader["NUM_CLIENTE"],
                        Complemento = (string)Conexao.dataReader["COM_CLIENTE"],
                        Bairro = (string)Conexao.dataReader["BAO_CLIENTE"],
                        Cidade = (string)Conexao.dataReader["CID_CLIENTE"],
                    },
                    ValorTotal = float.Parse(Conexao.dataReader["VAL_TOTAL"].ToString()),
                };
                listaPedidos.Add(pedido);
            }
            List<Pedido> temp = new List<Pedido>();
            foreach (var pedido in listaPedidos)
            {
                pedido.listaProdutosPedidos = ProdutoPedidoDAL.ListarProdutosPedidos(pedido.Id);
                temp.Add(pedido);
            }
            listaPedidos = temp;
            return listaPedidos;
        }

        public static void IncluirPedido(Pedido pedido)
        {
            if(pedido.Id > Conexao.GetID("_PEDIDO", "PEDIDOS"))
            {
                string insert = "insert into PEDIDOS(ID_PEDIDO, ID_CLIENTE, VAL_TOTAL)"
                    + $" values({pedido.Id}, {pedido.Cliente.Id}, {pedido.ValorTotal.ToString("F2").Replace(',', '.')})";

                Conexao.ExecuteNonQuery(Conexao.Command(insert));

                foreach(var prodPed in pedido.listaProdutosPedidos)
                {
                    string insertProdPed = "insert into PRODUTOSPEDIDO(ID_PEDIDO, ID_PRODUTO, QTD_PRODUTO, VAL_PEDIDO)"
                    + $" values({prodPed.Pedido}, {prodPed.Produto.Id}, {prodPed.Quantidade}, {prodPed.ValorPedido.ToString("F2").Replace(',', '.')})";

                    Conexao.ExecuteNonQuery(Conexao.Command(insertProdPed));
                }
            }
            else
            {
                string update = "UPDATE PEDIDOS SET"
                    + $" VAL_TOTAL = {pedido.ValorTotal.ToString("F2").Replace(',', '.')}"
                    + $" WHERE ID_PEDIDO = {pedido.Id}";
                Conexao.ExecuteNonQuery(Conexao.Command(update));

                string deleteProdPed = $"DELETE FROM PRODUTOSPEDIDO WHERE ID_PEDIDO = {pedido.Id}";

                Conexao.ExecuteNonQuery(Conexao.Command(deleteProdPed));

                foreach (var prodPed in pedido.listaProdutosPedidos)
                {
                    string insertProdPed = "insert into PRODUTOSPEDIDO(ID_PEDIDO, ID_PRODUTO, QTD_PRODUTO, VAL_PEDIDO)"
                    + $" values({prodPed.Pedido}, {prodPed.Produto.Id}, {prodPed.Quantidade}, {prodPed.ValorPedido.ToString("F2").Replace(',', '.')})";

                    Conexao.ExecuteNonQuery(Conexao.Command(insertProdPed));
                }
            }
        }

        public static void RemoverPedido(int id)
        {
            string deleteProdPed = $"DELETE FROM PRODUTOSPEDIDO WHERE ID_PEDIDO = {id}";

            Conexao.ExecuteNonQuery(Conexao.Command(deleteProdPed));

            string delete = $"DELETE FROM PEDIDOS WHERE ID_PEDIDO = {id}";

            Conexao.ExecuteNonQuery(Conexao.Command(delete));
        }

        public static float ObterValorTotalPedido(int idPedido)
        {
            return ListarPedidos().Find(x => x.Id == idPedido).listaProdutosPedidos.Sum(x => x.Produto.Valor * x.Quantidade);
        }

        public static int GetMaxId()
        {
            return Conexao.GetID("_PEDIDO", "PEDIDOS");
        }
    }
}