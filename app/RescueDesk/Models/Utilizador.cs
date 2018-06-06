using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RescueDesk.Models
{
    public class Utilizador
    {
        [DisplayName("Id Utilizador")]
        public int idUtilizador { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [DisplayName("Nr Contribuinte")]
        public int? nrcontribuinte { get; set; }

        [DisplayName("Foto")]
        public string foto { get; set; }

        [DisplayName("Tipo")]
        public int idtipo { get; set; }

        [DisplayName("Tipo")]
        public string tipoUtilizador { get; set; }

        [DisplayName("Nome")]
        public string nome { get; set; }
    }
}