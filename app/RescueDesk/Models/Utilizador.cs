﻿namespace RescueDesk.Models
{
    public class Utilizador
    {
        public int idUtilizador { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int? nrcontribuinte { get; set; }
        public string foto { get; set; }
        public int idtipo { get; set; }
    }
}