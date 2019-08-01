using MySql.Data.MySqlClient;
using PizzariaRush.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzariaRush.DAL
{
    public class ProdutoDAL
    {
        private static List<Produto> listaProdutos;
        public static List<Produto> ListarProdutos()
        {
            string select = "select p.ID_PRODUTO, c.ID_CAT, c.NM_CAT, p.NM_PRODUTO, p.VAL_PRODUTO from PRODUTOS AS p";
            select += " join categorias c";
            select += " where p.ID_CAT = c.ID_CAT";

            listaProdutos = new List<Produto>();

            Conexao.ExecuteReader(Conexao.Command(select));
            try
            {
                while (Conexao.dataReader.Read())
                {
                    Produto produto = new Produto
                    {
                        Id = (int)Conexao.dataReader["ID_PRODUTO"],
                        Categoria = new Categoria{ Id = (int)Conexao.dataReader["ID_CAT"], Nome = (string)Conexao.dataReader["NM_CAT"] },
                        Nome = (string)Conexao.dataReader["NM_PRODUTO"],
                        Valor = float.Parse(Conexao.dataReader["VAL_PRODUTO"].ToString())
                    };
                    listaProdutos.Add(produto);
                }
            }
            catch (MySqlException)
            {
                throw;
            }
            return listaProdutos;
        }

        public static void IncluirProduto(Produto produto)
        {
            if (produto.Id > Conexao.GetID("_PRODUTO", "PRODUTOS"))
            {
                string insert = "insert into PRODUTOS(ID_PRODUTO, ID_CAT, NM_PRODUTO, VAL_PRODUTO)";
                insert += $" values({produto.Id}, {produto.Categoria.Id}, '{produto.Nome}', {produto.Valor.ToString("F2").Replace(',', '.')})";
                Conexao.ExecuteNonQuery(Conexao.Command(insert));
            }
            else
            {
                string update = "update PRODUTOS set ID_CAT = " + produto.Categoria.Id + ", NM_PRODUTO = '" + produto.Nome + "', VAL_PRODUTO = " + produto.Valor.ToString("F2").Replace(',', '.');
                update += " WHERE ID_PRODUTO = " + produto.Id;

                Conexao.ExecuteNonQuery(Conexao.Command(update));
            }
        }

        public static void RemoverProduto(Produto produto)
        {
            string delete = $"delete from PRODUTOS where ID_PRODUTO = {produto.Id}";

            Conexao.ExecuteNonQuery(Conexao.Command(delete));
        }
        public static int GetMaxId()
        {
            return Conexao.GetID("_PRODUTO", "PRODUTOS");
        }
    }
}