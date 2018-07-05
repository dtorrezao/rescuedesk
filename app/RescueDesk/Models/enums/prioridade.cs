using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RescueDesk.Models.enums
{
    public enum prioridade
    {
        [Display(Name = "Crítica")]
        Critica,
        [Display(Name = "Alta")]
        Alta,
        [Display(Name = "Média")]
        Media,
        [Display(Name = "Baixa")]
        Baixa
    }
}