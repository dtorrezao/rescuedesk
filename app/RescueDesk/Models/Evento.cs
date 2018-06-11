using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RescueDesk.Models
{
    public class Evento
    {
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string url { get; set; }
        public string backgroundColor { get; set; }
        public bool allDay { get; set; }

        
    }
}