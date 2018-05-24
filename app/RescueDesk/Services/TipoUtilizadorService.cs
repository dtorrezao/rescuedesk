using MySql.Data.MySqlClient;
using RescueDesk.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RescueDesk.Services
{
    public class TipoUtilizadorService
    {
        private MySqlConnection Conn = new MySqlConnection(Utils.ConnectionString());

        public List<TipoUtilizador> ObterTipos()
        {
            List<TipoUtilizador> Tipos = new List<TipoUtilizador>();
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("SELECT * FROM tipoutilizador", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                TipoUtilizador TipoUser = ParseTipo(linha);
                Tipos.Add(TipoUser);
            }
            return Tipos;

        }


        public bool CreateTipoUser(TipoUtilizador TipoUser)
        {
            string query = "INSERT INTO `tipoutilizador` " +
               " (`idtipo`, `tipouser`) " +
               " VALUES (NULL, '" + TipoUser.tipouser + "')";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public bool UpdateTipo(TipoUtilizador TipoUser)
        {
            string query = "UPDATE `tipoutilizador`";
            query += "SET `tipouser` = '" + TipoUser.tipouser + "' " +
                 " WHERE `tipoutilizador`.`idtipo` = '" + TipoUser.idtipo.ToString() + "'";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public bool DeleteTipo(int id)
        {
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM tipoutilizador WHERE idtipo='" + id.ToString() + "'", this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }



        public TipoUtilizador ObterTipo(int id)
        {
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("Select * from tipoutilizador where idtipo='" + id.ToString() + "'", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                TipoUtilizador TipoUser = ParseTipo(linha);
                return TipoUser;
            }
            return null;
        }

        private static TipoUtilizador ParseTipo(DataRow linha)
        {
            TipoUtilizador tipos = new TipoUtilizador();
            tipos.idtipo = int.Parse(linha["idtipo"].ToString());
            tipos.tipouser = linha["tipouser"].ToString();
            return tipos;
        }



    }
}