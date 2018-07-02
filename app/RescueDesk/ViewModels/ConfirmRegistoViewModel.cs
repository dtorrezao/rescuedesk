using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RescueDesk.ViewModels
{
    public class ConfirmRegistoViewModel
    {
        public string Hash { get; set; }
        [DisplayName("Email")]
        public string Username { get; set; }
        public string Password { get; set; }
        [DisplayName("Confirmar Password")]
        public string ConfirmPassword { get; set; }
    }
}