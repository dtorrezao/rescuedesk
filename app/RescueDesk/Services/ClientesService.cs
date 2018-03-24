using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using RescueDesk.Models;

namespace RescueDesk.Services
{
    public class ClientesService
    {
        private MySqlConnection Conn = new MySqlConnection("server=localhost;database=rescuedesk;username=root;Convert Zero Datetime=True");

        public List<Cliente> ObterClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("Select * from clientes", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Cliente cliente= ParseCliente(linha);
                clientes.Add(cliente);
            }
            return clientes;

        }


        private static Cliente ParseCliente(DataRow linha)
        {
            Cliente cliente = new Cliente();
            cliente.nrcontribuinte = int.Parse(linha["nrcontribuinte"].ToString());
            cliente.nome = linha["nome"].ToString();
            cliente.morada = linha["morada"].ToString();
            cliente.codpostal =linha["codpostal"].ToString();
            cliente.contacto = int.Parse(linha["contacto"].ToString());
            cliente.email = linha["email"].ToString();
            cliente.obs = linha["obs"].ToString();
            return cliente;
        }
    }
}