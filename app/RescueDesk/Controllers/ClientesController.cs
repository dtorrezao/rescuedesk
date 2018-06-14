using RescueDesk.Models;
using RescueDesk.Services;
using RescueDesk.Utils;
using RescueDesk.ViewModels;
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
        UtilizadorService usrService = new UtilizadorService();
        TipoUtilizadorService tipoUtilizadorService = new TipoUtilizadorService();

        AddressService address = new AddressService();

        // GET: Clientes
        public ActionResult Index()
        {
            ClientesService servico = new ClientesService();

            return View(servico.ObterClientes());
        }

        public ActionResult Create()
        {
            ClientesService servico = new ClientesService();

            ClienteViewModel cvm = new ClienteViewModel();
            cvm.Cliente = servico.ObterClienteDefault();
            cvm.Utilizador = usrService.ObterUtilizadorDefault();

            cvm.Enderecos = new List<SelectListItem>() {
                new SelectListItem() { Text = "Selecione uma localidade" }
            };

            return View(cvm);
        }

        [HttpPost]
        public ActionResult Create(ClienteViewModel vm)
        {
            if (vm.CriarUtilizador)
            {
                vm.Utilizador = new Utilizador()
                {
                    ativo = true,
                    idtipo = 3,
                    nrcontribuinte = vm.Cliente.nrcontribuinte, 
                    email = vm.Cliente.email,
                };

                usrService.CreateUtilizador(vm.Utilizador);
               
                EmailService emailSvc = new EmailService();
                var link = "http://" + Request.Url.Authority + Url.Action("ConfirmarRegisto", "Funcionarios", new { hash = Criptografia.HashString(vm.Utilizador.email) });

                emailSvc.EnviarEmailRegisto(vm.Utilizador, link);
            }

            ClientesService servico = new ClientesService();
            if (servico.CreateCliente(vm.Cliente))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(vm);
            }
        }

        public ActionResult Edit(int id)
        {
            ClientesService servico = new ClientesService();

            ClienteViewModel cvm = new ClienteViewModel();
            cvm.Cliente = servico.ObterClienteDefault();
            cvm.Utilizador = usrService.ObterUtilizadorDefault();

            cvm.Enderecos = new List<SelectListItem>() {
                new SelectListItem() { Text = "Selecione uma localidade" }
            };

            return View(cvm);
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

        // GET: Default/Details/5
        public ActionResult Details(int id)
        {

            ClientesService servico = new ClientesService();

            return View(servico.ObterCliente(id));
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
            ViewBag.AvailableAddress = new List<SelectListItem>();
        }
    }
}