using RescueDesk.Models;
using RescueDesk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.Controllers
{
    [Authorize] // Todas as acções deste controlador estão disponiveis para os tipos de utlizador (apenas tem que fazer login)
    public class PedidosController : Controller
    {
        // GET: Pedidos
        public ActionResult Index()
        {
            PedidosService servico = new PedidosService();

            return View(servico.ObterPedidos());
        }

        public ActionResult Create()
        {
            PedidosService servico = new PedidosService();

            return View(servico.ObterPedidoDefault());
        }

        [HttpPost]
        public ActionResult Create(Pedido pedido)
        {
            PedidosService servico = new PedidosService();
            if (servico.CreatePedido(pedido))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(pedido);
            }
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


        public ActionResult Delete(int id)
        {
            PedidosService servico = new PedidosService();

            return View(servico.ObterPedido(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int idpedido)
        {
            PedidosService servico = new PedidosService();
            if (servico.DeletePedido(idpedido))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Delete", new { id = idpedido });
            }
        }
    }

}