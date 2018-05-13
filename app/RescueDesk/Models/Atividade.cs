using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RescueDesk.Models
{
    public class Atividade
    {
        [DisplayName("Id Atividade")]
        public int idatividade { get; set; }
        [DisplayName("Descrição")]
        public string descricao { get; set; }
        [DisplayName("Peso")]
        public int peso { get; set; }
    }
}