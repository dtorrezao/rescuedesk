﻿using RescueDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.ViewModels
{
    public class FuncionarioViewModel
    {
        public Funcionario Funcionario { get; set; }
        public Utilizador Utilizador { get; set; }
        public List<SelectListItem> Enderecos { get; set; }
        public List<SelectListItem> Departamentos { get; set; }
        public List<SelectListItem> TipoUtilizador { get; set; }
    }
}