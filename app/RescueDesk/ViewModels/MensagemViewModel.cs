using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RescueDesk.Models;

namespace RescueDesk.ViewModels
{
    public class MensagemViewModel
    {
        public Utilizador Utilizador { get; internal set; }
        public string Assunto { get; internal set; }
        public DateTime DataEnviada { get; internal set; }
        public string Link { get; internal set; }
    }
}