﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RescueDesk.Models
{
    public class Departamento
    {
        [DisplayName("ID")]
        public int iddept { get; set; }
        [DisplayName("Departamento")]
        public string dept { get; set; }

        [NotMapped]
        public bool PodeEliminar { get; set; }
    }
}