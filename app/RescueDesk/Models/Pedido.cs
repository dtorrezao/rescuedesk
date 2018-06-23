using RescueDesk.Models.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RescueDesk.Models
{
    public class Pedido
    {
        [DisplayName("ID")]
        public int idpedido { get; set; }
        [DisplayName("Assunto")]
        public string assunto { get; set; }
        [DisplayName("Descrição")]
        public string descricao { get; set; }
        [DisplayName("IDServiço")]
        public int? idatividade { get; set; }
        [DisplayName("Pedido")]
        public DateTime dtpedido { get; set; }
        [DisplayName("Lido")]
        public DateTime? dtlido { get; set; }
        [DisplayName("Marcação")]
        public DateTime? dtmarcado { get; set; }
        [DisplayName("Prioridade")]
        public prioridade? prioridade { get; set; }
        [DisplayName("Resolvido")]
        public DateTime? dtresolvido { get; set; }
        [DisplayName("Nº Contribuinte")]
        public int nrcontribuinte { get; set; }
        [DisplayName("IDFuncionário")]
        public int? idfuncionario { get; set; }
        [DisplayName("Observações")]
        public string obs { get; set; }
    }
}