using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RescueDesk.ViewModels
{
    public class ConfirmRegistoViewModel
    {
        public string Hash { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}