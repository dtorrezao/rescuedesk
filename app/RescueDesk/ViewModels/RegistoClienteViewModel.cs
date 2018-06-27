using RescueDesk.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.ViewModels
{
    public class RegistoClienteViewModel
    {
        public RegistoClienteViewModel()
        {
            Utilizador = new Utilizador();
        }

        public Cliente Cliente { get; set; }
        public Utilizador Utilizador { get; set; }
        public List<SelectListItem> Enderecos { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Confirme Password")]
        [DataType(DataType.Password)]
        public string ConfirmePassword { get; set; }
    }
}