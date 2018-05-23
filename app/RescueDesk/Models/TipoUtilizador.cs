using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RescueDesk.Models
{
    public class TipoUtilizador
    {
        [DisplayName("ID")]
        public int idtipo { get; set; }
        [DisplayName("Tipo de Utilizador")]
        public string tipouser { get; set; }
    }
}