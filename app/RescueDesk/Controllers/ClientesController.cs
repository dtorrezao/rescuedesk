using RescueDesk.Models;
using RescueDesk.Models.enums;
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
                new SelectListItem() { Text = "Introduza o seu código postal..." }
            };

            return View(cvm);
        }

        [HttpPost]
        public ActionResult Create(ClienteViewModel vm, HttpPostedFileBase foto)
        {
            ClientesService servico = new ClientesService();

            if (foto != null)
            {
                if (foto.ContentLength > 0)
                {
                    var fileName = System.IO.Path.GetFileName(foto.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath("~/images/"), fileName);
                    foto.SaveAs(path);

                    vm.Utilizador.foto = "/images/" + fileName;
                }
            }

            if (usrService.CreateUtilizador(vm.Utilizador))
            {
                vm.Cliente.nrcontribuinte = vm.Utilizador.nrcontribuinte;
                if (servico.CreateCliente(vm.Cliente))
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

                    return this.RedirectToAction("Index");
                }
            }
            return View(vm);
        }

        [AllowAnonymous]
        public ActionResult Edit(int id)
        {
            ClientesService servico = new ClientesService();

            ClienteViewModel cvm = new ClienteViewModel();
            cvm.Cliente = servico.ObterCliente(id);
            cvm.Utilizador = usrService.ObterUtilizadorByEmail(cvm.Cliente.email);

            var localidade = address.ObterLocalidade(cvm.Cliente.codpostal);
            cvm.CriarUtilizador = cvm.Utilizador != null;

            cvm.Enderecos = new List<SelectListItem>() { new SelectListItem() { Value = cvm.Cliente.codpostal, Text = string.Format("{0} - {1}", localidade.codpostal, localidade.nomeLocalidade), Selected = true } };

            return View(cvm);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Edit(ClienteViewModel cvm, HttpPostedFileBase foto)
        {
            ClientesService servico = new ClientesService();
            UtilizadorService UtilizadorService = new UtilizadorService();

            string userName = ControllerContext.HttpContext.User.Identity.Name;
            Utilizador utilizador = UtilizadorService.ObterUtilizadorByEmail(userName);

            if (foto != null)
            {
                if (foto.ContentLength > 0)
                {
                    var fileName = System.IO.Path.GetFileName(foto.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath("~/images/"), fileName);
                    foto.SaveAs(path);

                    cvm.Utilizador.foto = "/images/" + fileName;
                }
            }

            if (usrService.UpdateUtilizador(cvm.Utilizador))
            {
                cvm.Cliente.nrcontribuinte = cvm.Utilizador.nrcontribuinte;

                if (servico.UpdateCliente(cvm.Cliente))
                {
                    if (utilizador.idtipo == (int)TipoUtilizadorEnum.Cliente)
                    {
                        return this.RedirectToAction("Index", "Home");
                    }
                    return this.RedirectToAction("Index");
                }
            }

            return View(cvm);
        }

        // GET: Default/Details/5
        public ActionResult Details(int id)
        {

            ClientesService servico = new ClientesService();

            ClienteViewModel cvm = new ClienteViewModel();
            cvm.Cliente = servico.ObterClienteDefault();
            cvm.Utilizador = usrService.ObterUtilizadorDefault();

            cvm.Enderecos = new List<SelectListItem>() {
                new SelectListItem() { Text = "Introduza o seu código postal..." }
            };

            return View(cvm);
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