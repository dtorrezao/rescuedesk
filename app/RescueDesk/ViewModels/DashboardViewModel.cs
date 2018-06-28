using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RescueDesk.ViewModels
{
    public class DashboardViewModel
    {
        public int PedidosPorMes { get; set; }
        public string ClienteMaisPedidos { get; set; }
        public string ServicoMaisPedidos { get; set; }
        public string FuncionarioMaisPedidos { get; set; }

        public int[] PedidosPorMesData { get; set; }
        public string[] PedidosPorMesLabels { get;  set; }

        public int[] ClienteMaisPedidosData { get; set; }
        public string[] ClienteMaisPedidosLabels { get; set; }

        public int[] ServicoMaisPedidosData { get; set; }
        public string[] ServicoMaisPedidosLabels { get; set; }

        public int[] FuncionarioMaisPedidosData { get; set; }
        public string[] FuncionarioMaisPedidosLabels { get; set; }

        public ProfileCardViewModel ProfileCard { get;  set; }
    }
}