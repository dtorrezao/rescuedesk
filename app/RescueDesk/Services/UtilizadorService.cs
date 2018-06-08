using MySql.Data.MySqlClient;
using RescueDesk.Models;
using RescueDesk.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RescueDesk.Services
{
    public class UtilizadorService
    {
        private MySqlConnection Conn = new MySqlConnection(Utils.ConnectionString());

        public List<Utilizador> ObterUtilizadores()
        {
            List<Utilizador> Utilizadores = new List<Utilizador>();
            this.Conn.Open();

            string query = "Select u.*, tipouser, ";
            query += "case WHEN f.nome is null then c.nome ELSE f.nome END AS nome,";
            query += "CASE WHEN f.ativo IS NULL THEN 1 ELSE f.ativo END 'Ativo'";
            query += " from utilizadores u";
            query += " LEFT join funcionarios f on f.idUtilizador = u.idUtilizador";
            query += " LEFT join clientes c on c.nrcontribuinte = u.nrcontribuinte";
            query += " inner join tipoutilizador tu on u.idtipo = tu.idtipo";

            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Utilizador utilizador = ParseUtilizador(linha);
                Utilizadores.Add(utilizador);
            }
            return Utilizadores;
        }

        public Utilizador ObterUtilizadorByEmail(string user)
        {
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("Select * from utilizadores where email='" + user + "'", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Utilizador utilizador = ParseUtilizador(linha);
                return utilizador;
            }
            return null;
        }

        public Utilizador ObterUtilizadorDefault()
        {
            return new Utilizador()
            {
                idtipo = 2,
                foto = "default.jpg",
                password = "",
            };
        }

        public bool CreateUtilizador(Utilizador User)
        {
            string hashpwd = Criptografia.HashString(User.password ?? "");
            string query = "INSERT INTO `utilizadores` " +
               " (`email`, `password`,`nrcontribuinte`,`foto`, `idtipo`) " +
               " VALUES ('" + User.email + "','" + hashpwd + "'," + (User.nrcontribuinte.HasValue ? "'" + User.nrcontribuinte.Value + "'" : "NULL") + ",'" + User.foto + "','" + User.idtipo + "')";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();

            bool wasAdded = resultados > 0;

            query = "SELECT idUtilizador FROM `utilizadores` where email = '" + User.email + "'";
            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                User.idUtilizador = int.Parse(linha["idUtilizador"].ToString());
            }
            return wasAdded;
        }

        public bool ChangePassword(Utilizador utilizador)
        {
            string hashpwd = Criptografia.HashString(utilizador.password);
            string query = "UPDATE utilizadores " +
                           "SET password='" + hashpwd + "'" +
                           "WHERE idUtilizador ='" + utilizador.idUtilizador + "'";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public bool UpdateUtilizador(Utilizador utilizador)
        {
            string query = "UPDATE utilizadores " +
                           "SET foto = '" + utilizador.foto + "' " +
                           "WHERE idUtilizador ='" + utilizador.idUtilizador + "'";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public bool DeleteUtilizador(int id)
        {
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM utilizadores WHERE idUtilizador='" + id + "'", this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public Utilizador ObterUtilizador(int id)
        {
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("Select * from utilizadores where idUtilizador='" + id + "'", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Utilizador utilizador = ParseUtilizador(linha);
                return utilizador;
            }
            return null;
        }


        private Utilizador ParseUtilizador(DataRow linha)
        {
            Utilizador utilizador = new Utilizador();
            utilizador.idUtilizador = int.Parse(linha["idUtilizador"].ToString());
            utilizador.email = linha["email"].ToString();
            utilizador.password = linha["password"].ToString();
            if (linha["nrcontribuinte"].ToString() != "")
            {
                utilizador.nrcontribuinte = int.Parse(linha["nrcontribuinte"].ToString());
            }
            utilizador.foto = linha["foto"].ToString();
            utilizador.idtipo = int.Parse(linha["idtipo"].ToString());

            if (linha.Table.Columns.Contains("tipouser"))
            {
                utilizador.tipoUtilizador = linha["tipouser"].ToString();
            }

            if (linha.Table.Columns.Contains("nome"))
            {
                utilizador.nome = linha["nome"].ToString();
            }

            if (linha.Table.Columns.Contains("Ativo"))
            {
                utilizador.ativo = int.Parse(linha["Ativo"].ToString())==1?true:false;
            }

            return utilizador;
        }

        public bool VerificaUtilizador(string username, string password)
        {
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("SELECT PASSWORD, email FROM utilizadores as u WHERE u.email = '" + username + "'", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                return (linha["PASSWORD"].ToString().ToLower() == password.ToLower()) && (username == linha["email"].ToString());
            }

            return false;
        }

        public string ObterTipoUser(string username)
        {
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("SELECT tp.tipouser FROM utilizadores as u inner join tipoutilizador as tp on tp.idtipo = u.idtipo WHERE u.email = '" + username + "'", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                return linha["tipouser"].ToString();
            }

            return "";
        }
    }
}