using MySql.Data.MySqlClient;
using RescueDesk.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RescueDesk.Services
{
    public class AtividadeService
    {
        private MySqlConnection Conn = new MySqlConnection("server=localhost;database=rescuedesk;username=root;Convert Zero Datetime=True");

        public List<Atividade> ObterAtividades()
        {
            List<Atividade> atividades = new List<Atividade>();
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("SELECT * FROM tipoatividade", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Atividade atividade = ParseAtividade(linha);
                atividades.Add(atividade);
            }
            return atividades;

        }

        public bool CreateAtividade(Atividade atividade)
        {
            string query = "INSERT INTO tipoatividade " +
                           "(idatividade, atividade, peso) " +
                           "VALUES ('" + atividade.idatividade.ToString() + "', '" + atividade.descricao + "', '" + atividade.peso.ToString() + "')";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public bool UpdateAtividade(Atividade atividade)
        {
            string query = "UPDATE tipoatividade " +
                           "SET atividade='" + atividade.descricao + "', peso = '" + atividade.peso.ToString() + "' " +
                           "WHERE idatividade = '" + atividade.idatividade + "'";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public bool DeleteAtividade(int id)
        {
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM tipoatividade WHERE idatividade='" + id.ToString() + "'", this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public Atividade ObterAtividade(int id)
        {
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("Select * from tipoatividade where idatividade='" + id.ToString() + "'", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Atividade atividade = ParseAtividade(linha);
                return atividade;
            }
            return null;
        }

        private static Atividade ParseAtividade(DataRow linha)
        {
            Atividade atividade = new Atividade();
            atividade.idatividade = int.Parse(linha["idatividade"].ToString());
            atividade.descricao = linha["atividade"].ToString();
            atividade.peso = int.Parse(linha["peso"].ToString());
            return atividade;
        }

    }
}