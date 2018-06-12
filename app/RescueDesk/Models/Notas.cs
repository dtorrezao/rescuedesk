using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RescueDesk.Models
{
    public class Notas
    {
        [DisplayName("ID")]
        public int idnota { get; set; }
        [DisplayName("Titulo")]
        public string titulo { get; set; }
        [DisplayName("Corpo")]
        public string corpo { get; set; }
        [DisplayName("ID do Utilizador")]
        public int idUtilizador { get; set; }

    }
}