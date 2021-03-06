﻿using System;
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
        private MySqlConnection Conn = new MySqlConnection(Utils.ConnectionString());

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
                Cliente cliente = ParseCliente(linha);
                clientes.Add(cliente);
            }
            return clientes;

        }

        public Cliente ObterClienteDefault()
        {
            Cliente cliente = new Cliente();

            return cliente;
        }

        public bool CreateCliente(Cliente cliente)
        {
            string query = "INSERT INTO `clientes` " +
                " (`nrcontribuinte`, `nome`, `morada`, `codpostal`, `contacto`, `email`, `obs`)" +
                " VALUES('" + cliente.nrcontribuinte + "', '" + cliente.nome + "', ";

            if (string.IsNullOrEmpty(cliente.morada))
            {
                query += "NULL,";
            }
            else
            {
                query += "'" + cliente.morada + "',";
            }

            if (string.IsNullOrEmpty(cliente.codpostal))
            {
                query += "NULL,";
            }
            else if (cliente.codpostal == "Introduza o seu código postal...")
            {
                query += "NULL,";
            }
            else
            {
                query += "'" + cliente.codpostal + "',";
            }

            if (!cliente.contacto.HasValue)
            {
                query += "NULL,";
            }
            else
            {
                query += "'" + cliente.contacto + "',";
            }
            if (string.IsNullOrEmpty(cliente.email))
            {
                query += "NULL,";
            }
            else
            {
                query += "'" + cliente.email + "',";
            }

            query += "'" + cliente.obs + "')";

            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        private static Cliente ParseCliente(DataRow linha)
        {
            Cliente cliente = new Cliente();
            cliente.nrcontribuinte = int.Parse(linha["nrcontribuinte"].ToString());
            cliente.nome = linha["nome"].ToString();
            cliente.morada = linha["morada"].ToString();
            cliente.codpostal = linha["codpostal"].ToString();
            if (!string.IsNullOrEmpty(linha["contacto"].ToString()))
            {
                cliente.contacto = int.Parse(linha["contacto"].ToString());
            }
            cliente.email = linha["email"].ToString();
            cliente.obs = linha["obs"].ToString();
            return cliente;
        }

        public bool UpdateCliente(Cliente cliente)
        {
            string query = "UPDATE `clientes` ";
            query += "SET `nome` =  '" + cliente.nome + "', " +
                "`morada` =  '" + cliente.morada + "', ";

            if (cliente.codpostal != "Introduza o seu código postal...")
            {
                query += "`codpostal` =  '" + cliente.codpostal + "',";
            }

            query += " `contacto` = '" + cliente.contacto + "'," +
            " `email` = '" + cliente.email + "'," +
            " `obs` = '" + cliente.obs + "' " +
            "WHERE `clientes`.`nrcontribuinte` = '" + cliente.nrcontribuinte.ToString() + "'";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public Cliente ObterCliente(int id)
        {
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("Select * from clientes where nrcontribuinte='" + id.ToString() + "'", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Cliente cliente = ParseCliente(linha);
                return cliente;
            }
            return null;
        }
    }
}