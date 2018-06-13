using MySql.Data.MySqlClient;
using RescueDesk.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RescueDesk.Services
{
    public class ServicosService
    {
        private MySqlConnection Conn = new MySqlConnection(Utils.ConnectionString());

        public List<Servico> ObterServicos()
        {
            List<Servico> servicos = new List<Servico>();
            this.Conn.Open();

            string query = "SELECT DISTINCT ta.*, " +
                           "CASE WHEN p.idpedido IS NULL THEN 1 ELSE 0 END 'PodeEliminar' " +
                           "FROM tipoatividade ta " +
                           "LEFT JOIN pedidos p on ta.idatividade = p.idatividade";

            MySqlDataAdapter cmd = new MySqlDataAdapter(query, this.Conn);
            DataTable dados = new DataTable();
            cmd.Fill(dados);
            this.Conn.Close();

            foreach (DataRow linha in dados.Rows)
            {
                Servico servico = ParseServico(linha);
                servicos.Add(servico);
            }

            return servicos;
        }

        public bool CreateServico(Servico servico)
        {
            string query = "INSERT INTO tipoatividade " +
                           "(idatividade, atividade, peso) " +
                           "VALUES ('" + servico.idatividade.ToString() + "', '" + servico.descricao + "', '" + servico.peso.ToString() + "')";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public bool UpdateServico(Servico servico)
        {
            string query = "UPDATE tipoatividade " +
                           "SET atividade='" + servico.descricao + "', peso = '" + servico.peso.ToString() + "' " +
                           "WHERE idatividade = '" + servico.idatividade + "'";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public bool DeleteServico(int id)
        {
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM tipoatividade WHERE idatividade='" + id.ToString() + "'", this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }


        public Servico ObterServico(int id)
        {
            this.Conn.Open();
            MySqlDataAdapter cmd = new MySqlDataAdapter("Select * from tipoatividade where idatividade='" + id.ToString() + "'", this.Conn);
            DataTable dados = new DataTable();
            cmd.Fill(dados);
            this.Conn.Close();
            foreach (DataRow linha in dados.Rows)
            {
                Servico servico = ParseServico(linha);
                return servico;
            }
            return null;
        }

        private static Servico ParseServico(DataRow linha)
        {
            Servico servico = new Servico();
            servico.idatividade = int.Parse(linha["idatividade"].ToString());
            servico.descricao = linha["atividade"].ToString();
            servico.peso = int.Parse(linha["peso"].ToString());

            if (linha.Table.Columns.Contains("PodeEliminar"))
            {
                servico.PodeEliminar = linha["PodeEliminar"].ToString().Equals("1");
            }
            return servico;
        }
    }
}