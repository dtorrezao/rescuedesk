﻿using MySql.Data.MySqlClient;
using RescueDesk.Models;
using System.Collections.Generic;
using System.Data;

namespace RescueDesk.Services
{
    public class NotasService
    {
        private MySqlConnection Conn = new MySqlConnection(Utils.ConnectionString());

        public List<Nota> ObterNotas(Utilizador utilizador)
        {
            List<Nota> Notas = new List<Nota>();
            this.Conn.Open();

            string query = "SELECT * FROM notas WHERE idUtilizador= " + utilizador.idUtilizador;

            //if (utilizador.idtipo != (int)TipoUtilizadorEnum.Administrador)
            //{
            //    query += "where idUtilizador = '" + utilizador.idUtilizador;
            //}

            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, this.Conn);

            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Nota nota = ParseNota(linha);
                Notas.Add(nota);
            }
            return Notas;
        }

        public bool CreateNota(Nota nota, Utilizador utilizador)
        {
            nota.idUtilizador = utilizador.idUtilizador;

            string query = "INSERT INTO `notas` " +
               " (`idnota`, `titulo`, `corpo`, `idUtilizador`) " +
               "VALUES(null,'" + nota.titulo + "', '" + nota.corpo + "', '" + nota.idUtilizador.ToString() + "')";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public Nota ObterNota(int id, Utilizador utilizador)
        {
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("Select * from notas where idnota='" + id.ToString() + "' AND idUtilizador= "+utilizador.idUtilizador, this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Nota nota = ParseNota(linha);
                return nota;
            }
            return null;
        }

        public bool UpdateNota(Nota nota, Utilizador utilizador)
        {
            string query = "UPDATE `notas` ";
            query += "SET `titulo` = '" + nota.titulo + "', " +
                "`corpo` = '" + nota.corpo + "'" +
                 " WHERE `notas`.`idnota` = '" + nota.idnota.ToString() + "' AND idUtilizador= " + utilizador.idUtilizador;
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public bool DeleteNota(int id, Utilizador utilizador)
        {
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM notas where idnota='" + id.ToString() + "' AND idUtilizador= " + utilizador.idUtilizador, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        private static Nota ParseNota(DataRow linha)
        {
            Nota notas = new Nota();
            notas.idnota = int.Parse(linha["idnota"].ToString());
            notas.titulo = linha["titulo"].ToString();
            notas.corpo = linha["corpo"].ToString();
            notas.idUtilizador = int.Parse(linha["idUtilizador"].ToString());

            return notas;
        }
    }
}