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

        string selectQuery = "SELECT p.*, ta.atividade, f.nome FROM pedidos p " +
                "LEFT join funcionarios f on f.idfuncionario = p.idfuncionario " +
                "LEFT JOIN tipoatividade ta ON ta.idatividade = p.idatividade ";
        public List<Pedido> ObterPedidos(Utilizador utilizador, bool apenasOsDoUtilizador = false, bool ObterResolvidos = false)
        {
            List<Pedido> pedidos = new List<Pedido>();
            this.Conn.Open();

            string query = selectQuery;

            bool whereClause = false;

            if (utilizador.idtipo == (int)TipoUtilizadorEnum.Cliente)
            {
                query += " where p.nrcontribuinte= '" + utilizador.nrcontribuinte + "'";
                whereClause = true;
            }
            else if (utilizador.idtipo != (int)TipoUtilizadorEnum.Administrador || apenasOsDoUtilizador)
            {
                query += " where f.idutilizador= '" + utilizador.idUtilizador + "'";
                whereClause = true;
            }

            if (ObterResolvidos)
            {
                if (whereClause)
                {
                    query += " AND dtresolvido is NULL";
                }
                else
                {
                    query += " WHERE dtresolvido is NULL";
                }
            }

            query += " ORDER by  dtresolvido ASC, prioridade asc, dtpedido ASC";

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

        public List<Pedido> ObterPedidosPendentes(Utilizador utilizador)
        {
            List<Pedido> pedidos = new List<Pedido>();
            this.Conn.Open();


            string query = selectQuery;
            query += " WHERE p.idfuncionario is null";

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
               "VALUES(NULL, '" + pedido.assunto + "', '" + pedido.descricao + "', NULL, '" + pedido.dtpedido.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
               "NULL, NULL, '" + pedido.prioridade.ToString() + "', " +
               "NULL, '" + pedido.nrcontribuinte + "' ,NULL, '" + pedido.obs + "')";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        public bool UpdatePedido(Pedido pedido)
        {
            string query = "UPDATE `pedidos` ";
            query += "SET ";
            query += "`assunto` = '" + pedido.assunto + "', ";
            query += "`descricao` =  '" + pedido.descricao + "', ";

            if (pedido.idatividade != null)
            {
                query += "`idatividade` =  '" + pedido.idatividade + "', ";
            }

            if (pedido.dtlido != null)
            {
                query += " `dtlido` = '" + pedido.dtlido.Value.ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            if (pedido.dtmarcado != null)
            {
                query += " `dtmarcado` = '" + pedido.dtmarcado.Value.ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (pedido.prioridade != null)
            {
                query += "`prioridade` = '" + pedido.prioridade.ToString() + "',";
            }

            if (pedido.dtresolvido != null)
            {
                query += " `dtresolvido` = '" + pedido.dtresolvido.Value.ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            query += " `nrcontribuinte` = '" + pedido.nrcontribuinte + "',";

            if (pedido.idfuncionario != null)
            {
                query += " `idfuncionario` = '" + pedido.idfuncionario + "', ";
            }
            query += "`obs` = '" + pedido.obs + "' ";
            query += "WHERE `pedidos`.`idpedido` = '" + pedido.idpedido.ToString() + "'";
            this.Conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            int resultados = cmd.ExecuteNonQuery();
            this.Conn.Close();
            return resultados > 0;
        }

        private static Pedido ParsePedido(DataRow linha)
        {
            Pedido pedido = new Pedido();
            pedido.idpedido = int.Parse(linha["idpedido"].ToString());
            pedido.assunto = linha["assunto"].ToString();
            pedido.descricao = linha["descricao"].ToString();
            pedido.dtpedido = DateTime.Parse(linha["dtpedido"].ToString());
            pedido.nrcontribuinte = int.Parse(linha["nrcontribuinte"].ToString());

            if (!string.IsNullOrEmpty(linha["idatividade"].ToString()))
            {
                pedido.idatividade = int.Parse(linha["idatividade"].ToString());
            }

            if (!string.IsNullOrEmpty(linha["atividade"].ToString()))
            {
                pedido.atividade = linha["atividade"].ToString();
            }
            if (!string.IsNullOrEmpty(linha["dtmarcado"].ToString()))
            {
                pedido.dtmarcado = DateTime.Parse(linha["dtmarcado"].ToString());
            }

            if (!string.IsNullOrEmpty(linha["dtlido"].ToString()))
            {
                pedido.dtlido = DateTime.Parse(linha["dtlido"].ToString());
            }

            if (!string.IsNullOrEmpty(linha["dtresolvido"].ToString()))
            {
                pedido.dtresolvido = DateTime.Parse(linha["dtresolvido"].ToString());
            }

            if (!string.IsNullOrEmpty(linha["prioridade"].ToString()))
            {
                pedido.prioridade = (prioridade)Enum.Parse(typeof(prioridade), linha["prioridade"].ToString());
            }
            if (!string.IsNullOrEmpty(linha["idfuncionario"].ToString()))
            {
                pedido.idfuncionario = int.Parse(linha["idfuncionario"].ToString());
                pedido.funcionario = linha["nome"].ToString();
            }

            pedido.obs = linha["obs"].ToString();
            return pedido;
        }

        public Pedido ObterPedido(int id)
        {
            this.Conn.Open();

            string query = selectQuery;
            query += " where idpedido='" + id.ToString() + "' ";
            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, this.Conn);
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