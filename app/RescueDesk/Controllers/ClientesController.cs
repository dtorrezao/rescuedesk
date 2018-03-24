using RescueDesk.Models;
using RescueDesk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.Controllers
{
    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult Index()
        {
            ClientesService servico = new ClientesService();

            return View(servico.ObterClientes());
        }

        public ActionResult Create()
        {
            ClientesService servico = new ClientesService();

            return View(servico.ObterClienteDefault());
        }

        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            ClientesService servico = new ClientesService();
            if (servico.CreateCliente(cliente))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(cliente);
            }
        }
    }
}