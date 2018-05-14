using MySql.Data.MySqlClient;
using RescueDesk.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RescueDesk.Services
{
    public class UtilizadorService
    {
        private MySqlConnection Conn = new MySqlConnection("server=localhost;database=rescuedesk;username=root;Convert Zero Datetime=True");

        public List<Utilizador> ObterUtilizadores()
        {
            List<Utilizador> Utilizadores = new List<Utilizador>();
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("Select * from utilizadores", this.Conn);
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

        public bool CreateUtilizador(Utilizador User)
        {
            string query = "INSERT INTO `utilizadores` " +
               " (`email`, `password`,`nrcontribuinte`,`foto`, `idtipo`) " +
               " VALUES ('" + User.email + "','" + User.password + "','" + User.nrcontribuinte + "','" + User.foto + "','" + User.idtipo + "')";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public bool UpdateUtilizador(Utilizador utilizador)
        {
            string query = "UPDATE utilizadores " +
                           "SET password='" + utilizador.password + "', foto = '" + utilizador.foto + "' " +
                           "WHERE email ='" + utilizador.email+ "'";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public bool DeleteUtilizador(string id)
        {
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM utilizadores WHERE email='" + id + "'", this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public Utilizador ObterUtilizador(string id)
        {
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("Select * from utilizadores where email='" + id + "'", this.Conn);
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
            utilizador.email = linha["email"].ToString();
            utilizador.password = linha["password"].ToString();
            if (linha["nrcontribuinte"].ToString() != "")
            {
                utilizador.nrcontribuinte = int.Parse(linha["nrcontribuinte"].ToString());
            }
            utilizador.foto = linha["foto"].ToString();
            utilizador.idtipo = int.Parse(linha["idtipo"].ToString());

            return utilizador;
        }
    }
}