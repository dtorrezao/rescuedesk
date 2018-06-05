using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RescueDesk.Models
{
    public class Funcionario
    {
        [DisplayName("ID")]
        public int idfuncionario { get; set; }
        [DisplayName("Nome")]
        public string nome { get; set; }
        [DisplayName("Morada")]
        public string morada { get; set; }
        [DisplayName("Código Postal")]
        public string codpostal { get; set; }
        [DisplayName("Localidade")]
        public string localidade { get; set; }
        [DisplayName("Nº Dept.")]
        public int iddept { get; set; }
        [DisplayName("Departamento")]
        public string dept { get; set; }
        [DisplayName("Cargo")]
        public string cargo { get; set; }
        [DisplayName("Contacto")]
        public int contacto { get; set; }
        [DisplayName("Nº Utilizador")]
        public int idUtilizador { get; set; }
        [DisplayName("Email")]
        public string email { get; set; }
        [DisplayName("Ativo")]
        public bool ativo { get; set; }
        [DisplayName("Último Login")]
        public DateTime ultlogin { get; set; }
        [DisplayName("Observações")]
        public string obs { get; set; }
    }
}