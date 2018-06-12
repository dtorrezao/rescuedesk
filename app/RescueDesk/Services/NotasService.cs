using MySql.Data.MySqlClient;
using RescueDesk.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RescueDesk.Services
{
    public class NotasService
    {
        private MySqlConnection Conn = new MySqlConnection(Utils.ConnectionString());

        public List<Notas> ObterNotas()
        {
            List<Notas> Notas = new List<Notas>();
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("SELECT * FROM notas", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Notas nota = ParseNota(linha);
                Notas.Add(nota);
            }
            return Notas;
        }

        public bool CreateNota(Notas nota)
        {
            string query = "INSERT INTO `notas` " +
               " (`idtipo`, `titulo`, `corpo`, `idUtilizador`) " +
               "VALUES(Null,'" + nota.titulo + "', '" + nota.corpo+ "', '" + nota.idUtilizador.ToString() + "')";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }


        public Notas ObterNota (int id)
        {
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("Select * from notas where idnota='" + id.ToString() + "'", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Notas nota= ParseNota(linha);
                return nota;
            }
            return null;
        }

        public bool UpdateNota(Notas nota)
        {
            string query = "UPDATE `notas` ";
            query += "SET `titulo` = '" + nota.titulo + "', " +
                "`corpo` = '" + nota.corpo + "'" +
                 " WHERE `notas`.`idnota` = '" + nota.idnota.ToString() + "'";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public bool DeleteNota(int id)
        {
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM notas where idnota='" + id.ToString() + "'", this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        private static Notas ParseNota(DataRow linha)
        {
            Notas notas = new Notas();
            notas.idnota = int.Parse(linha["idnota"].ToString());
            notas.titulo = linha["titulo"].ToString();
            notas.corpo = linha["corpo"].ToString();
            notas.idUtilizador =int.Parse(linha["idUtilizador"].ToString());

            return notas;
        }
    }
}