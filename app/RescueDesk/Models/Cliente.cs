using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RescueDesk.Models
{
    public class Cliente
    {
        public int nrcontribuinte { get; set; }
        public string nome { get; set; }
        public string morada { get; set; }
        public string codpostal { get; set; }
        public int contacto { get; set; }
        public string email { get; set; }
        public string obs { get; set; }
    }
}