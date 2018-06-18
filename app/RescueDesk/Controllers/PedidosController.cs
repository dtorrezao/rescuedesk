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
        private Utilizador ObterUtilizador()
        {
            string userName = ControllerContext.HttpContext.User.Identity.Name;
            UtilizadorService UtilizadorService = new UtilizadorService();
            return UtilizadorService.ObterUtilizadorByEmail(userName);
        }

        // GET: Pedidos
        public ActionResult Index()
        {
            PedidosService servico = new PedidosService();

            return View(servico.ObterPedidos(this.ObterUtilizador()));
        }

        public ActionResult ListarPendidosPendentes()
        {
            PedidosService servico = new PedidosService();

            return View(servico.ObterPedidosPendentes(this.ObterUtilizador()));
        }
        public ActionResult AtribuirFuncionario(int id)
        {
            PedidosService servico = new PedidosService();

            return View(servico.ObterPedido(id));
        }
        [HttpPost]
        public ActionResult AtribuirFuncionario(Pedido pedido)
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