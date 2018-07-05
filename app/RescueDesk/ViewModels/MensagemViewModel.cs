using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RescueDesk.Models;

namespace RescueDesk.ViewModels
{
    public class MensagemViewModel
    {
        public Utilizador Utilizador { get; internal set; }
        public string Assunto { get; internal set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataEnviada { get; internal set; }
        public string Link { get; internal set; }
    }
}