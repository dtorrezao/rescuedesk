using Newtonsoft.Json;
using RescueDesk.Models;
using RescueDesk.Services;
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


        public string GetEvents(string start, string end)
        {
            var pedidosservice = new PedidosService();

            var pedidop = pedidosservice.ObterPedidos(this.ObterUtilizador());
            List<Evento> eventos = pedidop.Where(x => x.dtmarcado != null).Select(x => new Evento() { title = x.idpedido.ToString(), start = x.dtmarcado.Value }).ToList();
            return JsonConvert.SerializeObject(eventos);
        }

        private Utilizador ObterUtilizador()
        {
            string userName = ControllerContext.HttpContext.User.Identity.Name;
            UtilizadorService UtilizadorService = new UtilizadorService();
            return UtilizadorService.ObterUtilizadorByEmail(userName);
        }
    }
}