using MySql.Data.MySqlClient;
using RescueDesk.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace RescueDesk.Services
{
    public class DashboardService
    {
        private MySqlConnection Conn = new MySqlConnection(Utils.ConnectionString());

        public ChartViewModel ObterPedidosPorMes()
        {
            string query = "SELECT MONTH(dtpedido) as Mes, COUNT(dtpedido) Qtd FROM `pedidos` ";
            query += " WHERE YEAR(dtpedido)= YEAR(CURDATE())";
            query += " GROUP BY MONTH(dtpedido)";
            query += " ORDER BY MONTH(dtpedido)";

            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();

            List<KeyValuePair<int, int>> qtds = new List<KeyValuePair<int, int>>();
            foreach (DataRow linha in dados1.Rows)
            {
                var mes = int.Parse(linha["Mes"].ToString());
                var Qtd = linha["Qtd"].ToString();
                qtds.Add(new KeyValuePair<int, int>(mes, int.Parse(Qtd)));
            }

            for (int i = 1; i <= 12; i++)
            {
                if (!qtds.Any(z => z.Key == i))
                {
                    qtds.Add(new KeyValuePair<int, int>(i, 0));
                }
            }

            qtds = qtds.OrderBy(x => x.Key).ToList();
            var vm = new ChartViewModel();
            vm.Qtd = qtds.Select(x => x.Value).Sum().ToString();
            vm.Data = qtds.Select(x => x.Value).ToArray();
            vm.Labels = qtds.Select(x => Utils.UppercaseFirst(new DateTime(2000, x.Key, 1).ToString("MMMM", CultureInfo.CurrentCulture))).ToArray();

            return vm;
        }

        public ChartViewModel ObterFuncionarioMaisPedidos()
        {
            string query = "SELECT funcionarios.nome, COUNT(idpedido) as QTD";
            query += " FROM pedidos";
            query += " INNER JOIN funcionarios ON funcionarios.idfuncionario = pedidos.idfuncionario";
            query += " WHERE YEAR(dtpedido)= YEAR(CURDATE())";
            query += " GROUP BY pedidos.idfuncionario";
            query += " ORDER BY QTD DESC";
            query += " LIMIT 5";

            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();

            Dictionary<string, int> qtds = new Dictionary<string, int>();
            foreach (DataRow linha in dados1.Rows)
            {
                var nome = linha["nome"].ToString();
                var Qtd = linha["QTD"].ToString();
                qtds.Add(nome, int.Parse(Qtd));
            }

            var vm = new ChartViewModel();
            if (qtds.Any())
            {
                vm.Qtd = qtds.First().Key;
                vm.Data = qtds.Select(x => x.Value).ToArray();
                vm.Labels = qtds.Select(x => x.Key.ToString()).ToArray();
            }
            else
            {
                vm.Qtd = "";
                vm.Data = new int[] { };
                vm.Labels = new string[] { };
            }


            return vm;
        }

        public ChartViewModel ObterServicoMaisPedidos()
        {
            string query = "SELECT tipoatividade.atividade, COUNT(idpedido) as QTD";
            query += " FROM pedidos";
            query += " INNER JOIN tipoatividade ON tipoatividade.idatividade = pedidos.idatividade";
            query += " WHERE YEAR(dtpedido)= YEAR(CURDATE())";
            query += " GROUP BY pedidos.idatividade";
            query += " ORDER BY QTD DESC";
            query += " LIMIT 5";

            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();

            Dictionary<string, int> qtds = new Dictionary<string, int>();
            foreach (DataRow linha in dados1.Rows)
            {
                var nome = linha["atividade"].ToString();
                var Qtd = linha["QTD"].ToString();
                qtds.Add(nome, int.Parse(Qtd));
            }

            var vm = new ChartViewModel();
            if (qtds.Any())
            {
                vm.Qtd = qtds.First().Key;
                vm.Data = qtds.Select(x => x.Value).ToArray();
                vm.Labels = qtds.Select(x => x.Key.ToString()).ToArray();
            }
            else
            {
                vm.Qtd = "";
                vm.Data = new int[] { };
                vm.Labels = new string[] { };
            }


            return vm;
        }

        public ChartViewModel ObterClienteMaisPedidos()
        {
            string query = "SELECT clientes.nome, COUNT(idpedido) as QTD FROM pedidos";
            query += " INNER JOIN clientes ON clientes.nrcontribuinte = pedidos.nrcontribuinte";
            query += " WHERE YEAR(dtpedido)= YEAR(CURDATE())";
            query += " GROUP BY pedidos.nrcontribuinte";
            query += " ORDER BY QTD DESC";
            query += " LIMIT 5";

            this.Conn.Open();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter(query, this.Conn);
            DataTable dados1 = new DataTable();
            cmd1.Fill(dados1);
            this.Conn.Close();

            Dictionary<string, int> qtds = new Dictionary<string, int>();
            foreach (DataRow linha in dados1.Rows)
            {
                var nome = linha["nome"].ToString();
                var Qtd = linha["QTD"].ToString();
                qtds.Add(nome, int.Parse(Qtd));
            }

            var vm = new ChartViewModel();
            if (qtds.Any())
            {
                vm.Qtd = qtds.First().Key;
                vm.Data = qtds.Select(x => x.Value).ToArray();
                vm.Labels = qtds.Select(x => x.Key.ToString()).ToArray();
            }
            else
            {
                vm.Qtd = "";
                vm.Data = new int[] { };
                vm.Labels = new string[] { };
            }


            return vm;
        }

        /*
         *  
            
            
            
            
            
            
*/
    }
}
