using RescueDesk.Models.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RescueDesk.Models
{
    public class Localidade
    {
        [DisplayName("Código Postal")]
        public string codpostal { get; set; }
        [DisplayName("Localidade")]
        public string nomeLocalidade { get; set; }

        public static Localidade Parse(string line)
        {
            string[] words = line.Split(';');
            return new Localidade()
            {
                codpostal = words[14] + "-" + words[15],
                nomeLocalidade = words[16]
            };
        }
    }
}