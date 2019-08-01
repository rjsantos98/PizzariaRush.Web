using MySql.Data.MySqlClient;
using PizzariaRush.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzariaRush.DAL
{
    public class CategoriaDAL
    {
        private static List<Categoria> listaCategorias;
        public static List<Categoria> ListarCategorias()
        {
            string select = "select * from CATEGORIAS";
            listaCategorias = new List<Categoria>();

            Conexao.ExecuteReader(Conexao.Command(select));
            try
            {
                while (Conexao.dataReader.Read())
                {
                    Categoria categoria = new Categoria
                    {
                        Id = (int)Conexao.dataReader["ID_CAT"],
                        Nome = (string)Conexao.dataReader["NM_CAT"]
                    };
                    listaCategorias.Add(categoria);
                }
            }
            catch (MySqlException)
            {
                throw;
            }
            return listaCategorias;
        }

        public static void IncluirCategoria(Categoria categoria)
        {
            if (categoria.Id > Conexao.GetID("_CAT", "categorias"))
            {
                string insert = "insert into CATEGORIAS(ID_CAT, NM_CAT) values('" + categoria.Id + "', '" + categoria.Nome + "')";

                Conexao.ExecuteNonQuery(Conexao.Command(insert));
            }
            else
            {
                string update = "update CATEGORIAS set NM_CAT = '" + categoria.Nome + "' WHERE ID_CAT = " + categoria.Id;

                Conexao.ExecuteNonQuery(Conexao.Command(update));
            }
        }

        public static void RemoverCategoria(Categoria categoria)
        {
            string delete = $"delete from CATEGORIAS where ID_CAT = {categoria.Id}";

            Conexao.ExecuteNonQuery(Conexao.Command(delete));
        }

        public static int GetMaxId()
        {
            return Conexao.GetID("_CAT", "CATEGORIAS");
        }
    }
}