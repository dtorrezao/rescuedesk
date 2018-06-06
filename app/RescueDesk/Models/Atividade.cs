using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RescueDesk.Models
{
    public class Atividade
    {
        [DisplayName("ID")]
        public int idatividade { get; set; }
        [DisplayName("Descrição")]
        public string descricao { get; set; }
        [DisplayName("Peso")]
        public int peso { get; set; }

        [NotMapped]
        public bool PodeEliminar { get; set; }
    }
}