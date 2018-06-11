using Newtonsoft.Json;
using RescueDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        static List<Evento> data = new List<Evento>();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Calendario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdicionarEvento(Evento evento)
        {
            data.Add(evento);

            return RedirectToAction("Calendario");
        }

        public string GetEvents(string start, string end)
        {
            if (!data.Any())
            {
                data.Add(new Evento { title = "Teste", start = DateTime.Now, url = "http://www.google.com", backgroundColor = "#378006" });
                data.Add(new Evento { title = "Teste1", start = DateTime.Now.AddDays(2) });
            }

            return JsonConvert.SerializeObject(data);
        }
    }
}