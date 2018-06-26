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
        FuncionariosService servico = new FuncionariosService();
        UtilizadorService usrService = new UtilizadorService();
        AddressService address = new AddressService();
        DepartamentosService dptService = new DepartamentosService();
        TipoUtilizadorService tipoUtilizadorService = new TipoUtilizadorService();

        // GET: Funcionarios
        public ActionResult Index()
        {
            return View(servico.ObterFuncionarios());
        }

        // GET: Funcionarios/Details/5
        public ActionResult Details(int id)
        {
            FuncionarioViewModel vm = new FuncionarioViewModel();
            vm.Funcionario = servico.ObterFuncionario(id);
            vm.Utilizador = usrService.ObterUtilizador(vm.Funcionario.idUtilizador);

            var localidade = address.ObterLocalidade(vm.Funcionario.codpostal);

            vm.Enderecos = new List<SelectListItem>() { new SelectListItem() { Value = vm.Funcionario.codpostal, Text = string.Format("{0} - {1}", localidade.codpostal, localidade.nomeLocalidade), Selected = true } };
            vm.Departamentos = ListaDepartmentos(dptService);
            vm.TipoUtilizador = ListaTipoUtilizador(tipoUtilizadorService);

            return View(vm);
        }

        // GET: Funcionarios/Create
        public ActionResult Create()
        {
            FuncionarioViewModel vm = new FuncionarioViewModel();
            vm.Funcionario = servico.ObterFuncionarioDefault();
            vm.Utilizador = usrService.ObterUtilizadorDefault();
            vm.Enderecos = new List<SelectListItem>() { new SelectListItem() { Text = "Introduza o seu código postal..." } };
            vm.Departamentos = ListaDepartmentos(dptService);
            vm.TipoUtilizador = ListaTipoUtilizador(tipoUtilizadorService);
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


            if (usrService.CreateUtilizador(func.Utilizador))
            {
                func.Funcionario.idUtilizador = func.Utilizador.idUtilizador;
                if (servico.CreateFuncionario(func.Funcionario))
                {
                    EmailService emailSvc = new EmailService();
                    var link = "http://" + Request.Url.Authority + Url.Action("ConfirmarRegisto", "Funcionarios", new { hash = Criptografia.HashString(func.Utilizador.email) });

                    emailSvc.EnviarEmailRegisto(func.Utilizador, link);

                    return this.RedirectToAction("Index");
                }
            }

            return RedirectToAction("Create");
        }

        [AllowAnonymous]
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

        public ActionResult EditByIdUtilizador(int id)
        {
            FuncionarioViewModel vm = new FuncionarioViewModel();
            vm.Funcionario = servico.ObterFuncionarioByIdUtilizador(id);
            vm.Utilizador = usrService.ObterUtilizador(vm.Funcionario.idUtilizador);

            var localidade = address.ObterLocalidade(vm.Funcionario.codpostal);

            vm.Enderecos = new List<SelectListItem>() { new SelectListItem() { Value = vm.Funcionario.codpostal, Text = string.Format("{0} - {1}", localidade.codpostal, localidade.nomeLocalidade), Selected = true } };
            vm.Departamentos = ListaDepartmentos(dptService);
            vm.TipoUtilizador = ListaTipoUtilizador(tipoUtilizadorService);

            return View("Edit", vm);
        }

        public ActionResult Edit(int id)
        {
            FuncionarioViewModel vm = new FuncionarioViewModel();
            vm.Funcionario = servico.ObterFuncionario(id);
            vm.Utilizador = usrService.ObterUtilizador(vm.Funcionario.idUtilizador);

            var localidade = address.ObterLocalidade(vm.Funcionario.codpostal);

            vm.Enderecos = new List<SelectListItem>() { new SelectListItem() { Value = vm.Funcionario.codpostal, Text = string.Format("{0} - {1}", localidade.codpostal, localidade.nomeLocalidade), Selected = true } };
            vm.Departamentos = ListaDepartmentos(dptService);
            vm.TipoUtilizador = ListaTipoUtilizador(tipoUtilizadorService);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(FuncionarioViewModel func, HttpPostedFileBase foto)
        {
            if (Request.Form["Funcionario.ativo"] == "on")
            {
                func.Funcionario.ativo = true;
            }

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
            var List = new List<SelectListItem>();
            foreach (var item in dptService.ObterDepartamentos())
            {
                List.Add(new SelectListItem() { Text = item.dept, Value = item.iddept.ToString() });
            }
            return List;
        }

        private List<SelectListItem> ListaTipoUtilizador(TipoUtilizadorService utilizadorService)
        {
            //listar moradas disponiveis
            var ListTipoUser = new List<SelectListItem>();
            foreach (var item in utilizadorService.ObterTipos())
            {
                if (item.idtipo != (int)Models.enums.TipoUtilizadorEnum.Cliente)
                {
                    ListTipoUser.Add(new SelectListItem() { Text = item.tipouser, Value = item.idtipo.ToString() });
                }
            }
            return ListTipoUser;
        }
    }
}
