using RescueDesk.Models.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RescueDesk.Models
{
    public class Pedido
    {
        public int idpedido { get; set; }
        public string assunto { get; set; }
        public string descricao { get; set; }
        public int idatividade { get; set; }
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy hh:mm}",ApplyFormatInEditMode =true)]
        public DateTime dtpedido { get; set; }
        public DateTime dtlido { get; set; }
        public DateTime dtmarcado { get; set; }
        public prioridade prioridade { get; set; }
        public DateTime dtresolvido { get; set; }
        public int nrcontribuinte { get; set; }
        public int idfuncionario { get; set; }
        public string obs { get; set; }
    }
}