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
        public List<Pedido> ObterPedidos()
        {
            List<Pedido> pedidos = new List<Pedido>();
            MySqlConnection conn = new MySqlConnection("server=localhost;database=rescuedesk;username=root;Convert Zero Datetime=True");
            conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("Select * from pedidos", conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            conn.Close();
            foreach (DataRow linha in dados1.Rows)
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
                pedidos.Add(pedido);
            }
            return pedidos;

        }
    }
}