using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RescueDesk.Models
{
    public class Mensagem
    {
        [DisplayName("ID")]
        public int idmensagem { get; set; }
        [DisplayName("Assunto")]
        public string assunto { get; set; }
        [DisplayName("Mensagem")]
        public string corpo { get; set; }
        [DisplayName("Emissor")]
        public int emissor { get; set; }
        [DisplayName("Recetor")]
        public int recetor { get; set; }
        //[DisplayName("Email")]
        //[DataType(DataType.EmailAddress)]
        //public string email { get; set; }
        [DisplayName("Data de Envio")]
        public DateTime dtenviado { get; set; }
        [DisplayName("Lido")]
        public bool lido { get; set; }

    }
}