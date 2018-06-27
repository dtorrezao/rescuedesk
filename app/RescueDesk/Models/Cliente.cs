using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace RescueDesk.Models
{
    public class Cliente
    {
        [DisplayName("Nº Contribuinte")]
        public int? nrcontribuinte { get; set; }
        [DisplayName("Nome")]
        public string nome { get; set; }

        public string nomeAbreviado
        {
            get
            {
                int qtdCaracteres = 60;
                if (nome.Length > qtdCaracteres)
                {
                    string myString = nome.Substring(0, qtdCaracteres);

                    int index = myString.LastIndexOf(' ');

                    string outputString = myString.Substring(0, index);

                    return outputString + "...";
                }

                return nome;
            }
        }

        [DisplayName("Morada")]
        public string morada { get; set; }
        [DisplayName("Código Postal")]
        public string codpostal { get; set; }
        [DisplayName("Contacto")]
        public int? contacto { get; set; }
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [DisplayName("Observações")]
        public string obs { get; set; }
    }
}