using RescueDesk.Models;
using RescueDesk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.Controllers
{
    [Authorize(Roles = "Administrador, Funcionário")] // Todas as acções deste controlador estão disponiveis para os tipos de utlizador
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
            AddressService address = new AddressService();

            ListaEndereços(address);

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
                AddressService address = new AddressService();
                
                ListaEndereços(address);

                return View(cliente);
            }
        }

        public ActionResult Edit(int id)
        {
            ClientesService servico = new ClientesService();
            AddressService address = new AddressService();
            
            ListaEndereços(address);

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
                AddressService address = new AddressService();
                
                ListaEndereços(address);

                return View(cliente);
            }
        }

        private void ListaUtilizadores(UtilizadorService utilizadorService)
        {
            //lsitar emails disponiveis
            var AvailableEmails = new List<SelectListItem>();
            foreach (var item in utilizadorService.ObterUtilizadores())
            {
                AvailableEmails.Add(new SelectListItem() { Text = item.email, Value = item.email });
            }
            ViewBag.AvailableEmails = AvailableEmails;
        }

        private void ListaEndereços(AddressService address)
        {
            //listar moradas disponiveis
            var AvailableAddress = new List<SelectListItem>();
            foreach (var item in address.ObterLocalidades())
            {
                AvailableAddress.Add(new SelectListItem() { Text = item.codpostal + " - " + item.nomeLocalidade, Value = item.codpostal });
            }
            ViewBag.AvailableAddress = AvailableAddress;
        }
    }
}