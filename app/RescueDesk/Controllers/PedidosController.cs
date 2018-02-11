using RescueDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos
        public ActionResult Index()
        {
            List<Pedido> pedidos = new List<Pedido>();
            Pedido xpto = new Pedido();

            xpto.id = 1;

            pedidos.Add(xpto);

            return View(pedidos);
        }
    }
}