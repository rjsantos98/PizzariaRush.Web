using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzariaRush.DAL
{
    public class Conexao
    {
        private static MySqlConnection con;
        public static MySqlDataReader dataReader;
        private static MySqlConnection AbrirConexao()
        {
            con = new MySqlConnection("server=localhost;user id=jrdev;password=Pass*JR;persistsecurityinfo=True;database=pizzariaproject");
            con.Open();
            return con;
        }

        private static MySqlConnection FecharConexao()
        {
            con = new MySqlConnection("server=localhost;user id=jrdev;password=Pass*JR;persistsecurityinfo=True;database=pizzariaproject");
            con.Close();
            return con;
        }

        public static MySqlCommand Command(string query)
        {
            MySqlCommand _cmd;
            try
            {
                _cmd = new MySqlCommand(query, AbrirConexao());
            }
            finally
            {
                FecharConexao();
            }
            return _cmd;
        }

        public static void ExecuteNonQuery(MySqlCommand comando)
        {
            try
            {
                comando.ExecuteNonQuery();
            }
            finally
            {
                FecharConexao();
            }
        }

        public static void ExecuteReader(MySqlCommand comando)
        {
            try
            {
                dataReader = comando.ExecuteReader();
            }
            finally
            {
                FecharConexao();
            }
        }

        public static int GetID(string coluna, string tabela)
        {
            int id = -1;
            string SQL;
            try
            {
                SQL = "SELECT IFNULL(MAX(ID" + coluna + "), 0) FROM " + tabela + ";";
                MySqlCommand comando = new MySqlCommand(SQL, AbrirConexao());
                id = Convert.ToInt32(comando.ExecuteScalar());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
            return id;
        }

    }
}