using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RescueDesk.Models
{
    public class Utilizador
    {
        [DisplayName("ID")]
        public int idUtilizador { get; set; }

        [DisplayName("Nome")]
        public string nome { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [DisplayName("Nº Contribuinte")]
        public int? nrcontribuinte { get; set; }

        [DisplayName("Foto")]
        public string foto { get; set; }

        [DisplayName("Tipo")]
        public int idtipo { get; set; }

        [DisplayName("Tipo de Utilizador")]
        public string tipoUtilizador { get; set; }

        [DisplayName("Ativo")]
        public bool ativo { get; set; }
    }
}