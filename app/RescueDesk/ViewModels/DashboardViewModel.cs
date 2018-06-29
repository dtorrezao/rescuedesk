using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RescueDesk.Models;

namespace RescueDesk.ViewModels
{
    public class DashboardViewModel
    {
        public ChartViewModel PedidosPorMes { get; set; }
        public ChartViewModel ClienteMaisPedidos { get; set; }
        public ChartViewModel ServicoMaisPedidos { get; set; }
        public ChartViewModel FuncionarioMaisPedidos { get; set; }
        public ProfileCardViewModel ProfileCard { get;  set; }
        public List<Pedido> PedidosTop5 { get; internal set; }
    }
}