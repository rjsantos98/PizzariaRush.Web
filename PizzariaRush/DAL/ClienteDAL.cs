using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzariaRush.Models;
using MySql.Data.MySqlClient;

namespace PizzariaRush.DAL
{
    public class ClienteDAL
    {
        private static List<Cliente> listaClientes;

        public static List<Cliente> ListarClientes()
        {
            string select = "select * from CLIENTES";

            listaClientes = new List<Cliente>();

            Conexao.ExecuteReader(Conexao.Command(select));
            try
            {
                while (Conexao.dataReader.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        Id = (int)Conexao.dataReader["ID_CLIENTE"],
                        Nome = (string)Conexao.dataReader["NM_CLIENTE"],
                        Telefone = (string)Conexao.dataReader["TEL_CLIENTE"],
                        Endereco = (string)Conexao.dataReader["END_CLIENTE"],
                        Numero = (string)Conexao.dataReader["NUM_CLIENTE"],
                        Complemento = (string)Conexao.dataReader["COM_CLIENTE"],
                        Bairro = (string)Conexao.dataReader["BAO_CLIENTE"],
                        Cidade = (string)Conexao.dataReader["CID_CLIENTE"]
                    };
                    listaClientes.Add(cliente);
                }
            }
            catch (MySqlException)
            {
                throw;
            }
            return listaClientes;
        }

        public static void IncluirCliente(Cliente cliente)
        {
            if (cliente.Id > Conexao.GetID("_CLIENTE", "CLIENTES"))
            {
                string insert = "insert into CLIENTES(ID_CLIENTE, NM_CLIENTE, TEL_CLIENTE, END_CLIENTE, NUM_CLIENTE, COM_CLIENTE, BAO_CLIENTE, CID_CLIENTE)";
                insert += $" values({cliente.Id}, '{cliente.Nome}', '{cliente.Telefone}', '{cliente.Endereco}', '{cliente.Numero}', '{cliente.Complemento}', '{ cliente.Bairro}', '{cliente.Cidade}')";

                Conexao.ExecuteNonQuery(Conexao.Command(insert));
            }
            else
            {
                string update = $"update CLIENTES set NM_CLIENTE = '{cliente.Nome}', " +
                    $"TEL_CLIENTE = '{cliente.Telefone}', " +
                    $"END_CLIENTE = '{cliente.Endereco}', " +
                    $"NUM_CLIENTE = '{cliente.Numero}', " +
                    $"COM_CLIENTE = '{cliente.Complemento}', " +
                    $"BAO_CLIENTE = '{cliente.Bairro}', " +
                    $"CID_CLIENTE = '{cliente.Cidade}' " +
                    $"where ID_CLIENTE = {cliente.Id};";

                Conexao.ExecuteNonQuery(Conexao.Command(update));
            }
        }

        public static Cliente ChecarCliente(string telefone)
        {
            Cliente getCliente = new Cliente();
            string select = $"select * from CLIENTES WHERE TEL_CLIENTE LIKE '%{telefone}%'";

            Conexao.ExecuteReader(Conexao.Command(select));
            while (Conexao.dataReader.Read())
            {
                getCliente.Id = (int)Conexao.dataReader["ID_CLIENTE"];
                getCliente.Nome = (string)Conexao.dataReader["NM_CLIENTE"];
                getCliente.Telefone = (string)Conexao.dataReader["TEL_CLIENTE"];
                getCliente.Endereco = (string)Conexao.dataReader["END_CLIENTE"];
                getCliente.Numero = (string)Conexao.dataReader["NUM_CLIENTE"];
                getCliente.Complemento = (string)Conexao.dataReader["COM_CLIENTE"];
                getCliente.Bairro = (string)Conexao.dataReader["BAO_CLIENTE"];
                getCliente.Cidade = (string)Conexao.dataReader["CID_CLIENTE"];
            }
            return getCliente;
        }
        public static void RemoverCliente(Cliente cliente)
        {
            string delete = $"delete from CLIENTES where ID_CLIENTE = {cliente.Id}";

            Conexao.ExecuteNonQuery(Conexao.Command(delete));
        }
        public static int GetMaxId()
        {
            return Conexao.GetID("_CLIENTE", "CLIENTES");
        }
    }
}