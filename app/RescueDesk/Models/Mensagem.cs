using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RescueDesk.Models
{
    public class Mensagem
    {
        [DisplayName("Recetor")]
        public string recetorEmail { get; set; }
        [DisplayName("Emissor")]
        public string emissorEmail { get; set; }

        [DisplayName("ID")]
        public int idmensagem { get; set; }
        [DisplayName("Assunto")]
        public string assunto { get; set; }
        [DisplayName("Mensagem")]
        public string corpo { get; set; }
        

        public string mensagemAbreviado
        {
            get
            {
                int qtdCaracteres = 110;
                if (corpo.Length > qtdCaracteres)
                {
                    string myString = corpo.Substring(0, qtdCaracteres);
              

                    return myString + "...";
                }

                return corpo;
            }
        }
        [DisplayName("Emissor")]
        [DataType(DataType.EmailAddress)]
        public int emissor { get; set; }
        [DisplayName("Recetor")]
        [DataType(DataType.EmailAddress)]
        public int recetor { get; set; }
        //[DisplayName("Email")]
        //[DataType(DataType.EmailAddress)]
        //public string email { get; set; }
        [DisplayName("Enviado")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime dtenviado { get; set; }
        [DisplayName("Lido")]
        public bool lido { get; set; }

    }
}