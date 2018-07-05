using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RescueDesk.Models.enums
{
    public enum TipoUtilizadorEnum
    {
        [Display(Name = "Administrador")]
        Administrador = 1,

        [Display(Name = "Funcionário")]
        Funcionário = 2,

        [Display(Name = "Cliente")]
        Cliente = 3
    }
}