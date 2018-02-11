using RescueDesk.Models;
using RescueDesk.Services;
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
            PedidosService servico = new PedidosService();

            return View(servico.ObterPedidos());
        }

        public ActionResult Edit(int id)
        {
            PedidosService servico = new PedidosService();

            return View(servico.ObterPedido(id));
        }

        [HttpPost]
        public ActionResult Edit(Pedido pedido)
        {
            PedidosService servico = new PedidosService();
            if (servico.UpdatePedido(pedido))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(pedido);
            }
        }
    }

}