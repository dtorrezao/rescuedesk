using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                int qtdCaracteres = 60;
                if (corpo.Length > qtdCaracteres)
                {
                    string myString = corpo.Substring(0, qtdCaracteres);
              

                    return myString + "...";
                }

                return corpo;
            }
        }
        [DisplayName("Para")]
        public int emissor { get; set; }
        [DisplayName("De")]
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