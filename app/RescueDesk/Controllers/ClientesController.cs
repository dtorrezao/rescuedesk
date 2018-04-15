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
            UtilizadorService utilizadorService = new UtilizadorService();
            AddressService address = new AddressService();

            //lsitar emails disponiveis
            var AvailableEmails = new List<SelectListItem>();
            foreach (var item in utilizadorService.ObterUtilizadores())
            {
                AvailableEmails.Add(new SelectListItem() { Text = item.email, Value = item.email });
            }
            ViewBag.AvailableEmails = AvailableEmails;

            //listar moradas disponiveis
            var AvailableAddress = new List<SelectListItem>();
            foreach (var item in address.ObterLocalidades())
            {
                AvailableAddress.Add(new SelectListItem() { Text = item.codpostal + " - " + item.nomeLocalidade, Value = item.codpostal });
            }
            ViewBag.AvailableAddress = AvailableAddress;

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

        public ActionResult Edit(int id)
        {
            ClientesService servico = new ClientesService();

            return View(servico.ObterCliente(id));
        }

        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            ClientesService servico = new ClientesService();
            if (servico.UpdateCliente(cliente))
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