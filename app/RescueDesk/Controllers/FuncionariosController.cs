using RescueDesk.Models;
using RescueDesk.Services;
using RescueDesk.Utils;
using RescueDesk.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class FuncionariosController : Controller
    {
        // GET: Funcionarios
        public ActionResult Index()
        {
            FuncionariosService servico = new FuncionariosService();

            return View(servico.ObterFuncionarios());
        }

        // GET: Funcionarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Funcionarios/Create
        public ActionResult Create()
        {
            FuncionariosService servico = new FuncionariosService();
            UtilizadorService usrService = new UtilizadorService();
            AddressService address = new AddressService();
            DepartamentosService dptService = new DepartamentosService();

            FuncionarioViewModel vm = new FuncionarioViewModel();
            vm.Funcionario = servico.ObterFuncionarioDefault();
            vm.Utilizador = usrService.ObterUtilizadorDefault();
            vm.Enderecos = ListaEndereços(address);
            vm.Departamentos = ListaDepartmentos(dptService);
            return View(vm);
        }

        // POST: Departamentos/Create
        [HttpPost]
        public ActionResult Create(FuncionarioViewModel func, HttpPostedFileBase foto)
        {
            if (foto != null)
            {
                if (foto.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(foto.FileName);
                    var path = Path.Combine(Server.MapPath("~/images/"), fileName);
                    foto.SaveAs(path);

                    func.Utilizador.foto = "/images/" + fileName;
                }
            }

            FuncionariosService servico = new FuncionariosService();
            UtilizadorService usrService = new UtilizadorService();
            if (usrService.CreateUtilizador(func.Utilizador))
            {
                func.Funcionario.idUtilizador = func.Utilizador.idUtilizador;
                if (servico.CreateFuncionario(func.Funcionario))
                {
                    this.EnviarEmailRegisto(func.Utilizador);

                    return this.RedirectToAction("Index");
                }
            }

            return RedirectToAction("Create");
        }

        private void EnviarEmailRegisto(Utilizador utilizador)
        {
            var mail = new MailMessage();
            var link = "http://" + Request.Url.Authority + Url.Action("ConfirmarRegisto", "Funcionarios", new { hash = Criptografia.HashString(utilizador.email) });

            mail.Body = "Bla Bla Bla " + link + " bla bla bla";
            mail.Subject = "Register";
            mail.To.Add(new MailAddress(utilizador.email));

            EmailService emailSvc = new EmailService();
            emailSvc.EnviarEmail(mail);
        }

        public ActionResult ConfirmarRegisto(string hash)
        {
            ConfirmRegistoViewModel viewModel = new ConfirmRegistoViewModel();
            viewModel.Hash = hash;
            return View(viewModel);
        }

        [HttpPost]
        [ActionName("ConfirmarRegisto")]
        public ActionResult ConfirmarRegistoA(ConfirmRegistoViewModel utilizador)
        {
            string hashedUser = Criptografia.HashString(utilizador.Username);

            if (utilizador.Hash == hashedUser)
            {
                if (utilizador.Password == utilizador.ConfirmPassword)
                {
                    UtilizadorService usrService = new UtilizadorService();
                    var user = usrService.ObterUtilizadorByEmail(utilizador.Username);
                    user.password = utilizador.Password;
                    usrService.ChangePassword(user);

                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            FuncionariosService servico = new FuncionariosService();
            UtilizadorService usrService = new UtilizadorService();
            AddressService address = new AddressService();
            DepartamentosService dptService = new DepartamentosService();

            FuncionarioViewModel vm = new FuncionarioViewModel();
            vm.Funcionario = servico.ObterFuncionario(id);
            vm.Utilizador = usrService.ObterUtilizador(vm.Funcionario.idUtilizador);
            vm.Enderecos = ListaEndereços(address);
            vm.Departamentos = ListaDepartmentos(dptService);
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(FuncionarioViewModel func, HttpPostedFileBase foto)
        {
            if (foto != null)
            {
                if (foto.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(foto.FileName);
                    var path = Path.Combine(Server.MapPath("~/images/"), fileName);
                    foto.SaveAs(path);

                    func.Utilizador.foto = "/images/" + fileName;
                }
            }

            FuncionariosService servico = new FuncionariosService();
            UtilizadorService usrService = new UtilizadorService();
            if (usrService.UpdateUtilizador(func.Utilizador))
            {
                func.Funcionario.idUtilizador = func.Utilizador.idUtilizador;
                if (servico.UpdateFuncionario(func.Funcionario))
                {
                    return this.RedirectToAction("Index");
                }
            }

            return RedirectToAction("Edit", new { id = func.Funcionario.idfuncionario });
        }

        private List<SelectListItem> ListaDepartmentos(DepartamentosService dptService)
        {
            //listar moradas disponiveis
            var AvailableAddress = new List<SelectListItem>();
            foreach (var item in dptService.ObterDepartamentos())
            {
                AvailableAddress.Add(new SelectListItem() { Text = item.dept, Value = item.iddept.ToString() });
            }
            return AvailableAddress;
        }

        private List<SelectListItem> ListaEndereços(AddressService address)
        {
            //listar moradas disponiveis
            var AvailableAddress = new List<SelectListItem>();
            foreach (var item in address.ObterLocalidades().Take(100))
            {
                AvailableAddress.Add(new SelectListItem() { Text = item.codpostal + " - " + item.nomeLocalidade, Value = item.codpostal });
            }
            return AvailableAddress;
        }
    }
}
