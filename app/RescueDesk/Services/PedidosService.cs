using MySql.Data.MySqlClient;
using RescueDesk.Models;
using RescueDesk.Models.enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RescueDesk.Services
{
    public class PedidosService
    {
        private MySqlConnection Conn = new MySqlConnection(Utils.ConnectionString());

        public List<Pedido> ObterPedidos()
        {
            List<Pedido> pedidos = new List<Pedido>();
            this.Conn.Open();

            string query = "SELECT * FROM `pedidos`";
            
            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Pedido pedido = ParsePedido(linha);
                pedidos.Add(pedido);
            }
            return pedidos;

        }


        public Pedido ObterPedidoDefault()
        {
            Pedido pedido = new Pedido();
            pedido.prioridade = prioridade.Media;
            pedido.dtpedido = DateTime.Now;
            return pedido;
        }

        public bool CreatePedido(Pedido pedido)
        {
            string query = "INSERT INTO `pedidos` " +
               " (`idpedido`, `assunto`, `descricao`, `idatividade`, `dtpedido`, `dtlido`, `dtmarcado`, `prioridade`, `dtresolvido`, `nrcontribuinte`, `idfuncionario`, `obs`) " +
               "VALUES(NULL, '" + pedido.assunto + "', '" + pedido.descricao + "', '" + pedido.idatividade.ToString() + "', '" + pedido.dtpedido.ToString("yyyy-MM-dd hh:mm:ss") + "', " +
               "'" + pedido.dtlido.ToString("yyyy-MM-dd hh:mm:ss") + "', '" + pedido.dtmarcado.ToString("yyyy-MM-dd hh:mm:ss") + "', '" + pedido.prioridade.ToString() + "', " +
               "'" + pedido.dtresolvido.ToString("yyyy-MM-dd hh:mm:ss") + "', '" + pedido.nrcontribuinte + "' ,'" + pedido.idfuncionario + "', '" + pedido.obs + "')";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public bool UpdatePedido(Pedido pedido)
        {
            string query = "UPDATE `pedidos` ";
            query += "SET `assunto` = '" + pedido.assunto + "', " +
                "`descricao` =  '" + pedido.descricao + "', " +
                "`idatividade` =  '" + pedido.idatividade + "', " +
                "`dtpedido` =  '" + pedido.dtpedido.ToString("yyyy-MM-dd hh:mm:ss") + "'," +
                " `dtlido` = '" + pedido.dtlido.ToString("yyyy-MM-dd hh:mm:ss") + "'," +
                " `dtmarcado` = '" + pedido.dtmarcado.ToString("yyyy-MM-dd hh:mm:ss") + "'," +
                "`prioridade` = '" + pedido.prioridade.ToString() + "'," +
                " `dtresolvido` = '" + pedido.dtresolvido.ToString("yyyy-MM-dd hh:mm:ss") + "'," +
                " `nrcontribuinte` = '" + pedido.nrcontribuinte + "'," +
                " `idfuncionario` = '" + pedido.idfuncionario + "', " +
                "`obs` = '" + pedido.obs + "' " +
                "WHERE `pedidos`.`idpedido` = '" + pedido.idpedido.ToString() + "'";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados=cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        private static Pedido ParsePedido(DataRow linha)
        {
            Pedido pedido = new Pedido();
            pedido.idpedido = int.Parse(linha["idpedido"].ToString());
            pedido.assunto = linha["assunto"].ToString();
            pedido.descricao = linha["descricao"].ToString();
            pedido.idatividade = int.Parse(linha["idatividade"].ToString());
            pedido.dtpedido = DateTime.Parse(linha["dtpedido"].ToString());
            pedido.dtlido = DateTime.Parse(linha["dtlido"].ToString());
            pedido.dtmarcado = DateTime.Parse(linha["dtmarcado"].ToString());
            pedido.prioridade = (prioridade)Enum.Parse(typeof(prioridade), linha["prioridade"].ToString());
            pedido.dtresolvido = DateTime.Parse(linha["dtresolvido"].ToString());
            pedido.nrcontribuinte = int.Parse(linha["nrcontribuinte"].ToString());
            pedido.idfuncionario = int.Parse(linha["idfuncionario"].ToString());
            pedido.obs = linha["obs"].ToString();
            return pedido;
        }

        public Pedido ObterPedido(int id)
        {
            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("Select * from pedidos where idpedido='" + id.ToString() + "'", this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();
            foreach (DataRow linha in dados1.Rows)
            {
                Pedido pedido = ParsePedido(linha);
                return pedido;
            }
            return null;
        }

        public bool DeletePedido(int id)
        {
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM pedidos where idpedido='" + id.ToString() + "'", this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }
    }
}