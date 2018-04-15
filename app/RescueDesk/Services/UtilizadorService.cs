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

        private Utilizador ParseUtilizador(DataRow linha)
        {
               Utilizador utilizador = new Utilizador();
                utilizador.email = linha["email"].ToString();
                utilizador.password =linha["password"].ToString();
                utilizador.foto = linha["foto"].ToString();
                utilizador.idtipo =int.Parse(linha["idtipo"].ToString());

                return utilizador;            
        }
    }
}