using Newtonsoft.Json;
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
using System.Web.Security;


namespace RescueDesk.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UtilizadoresController : Controller
    {
        static string mensagem;

        UtilizadorService usrService = new UtilizadorService();
        TipoUtilizadorService tipoUtilizadorService = new TipoUtilizadorService();

        AddressService address = new AddressService();

        // GET: Utilizadores
        public ActionResult Index()
        {
            return View();
        }

        public static readonly string[] ChavesMestre = new string[] { "keyUsers" };
        public string ObterUtilizadores()
        {
            UtilizadorService UtilizadorService = new UtilizadorService();
            List<Utilizador> utilizadores = new List<Utilizador>();

            ////https://www.devmedia.com.br/cache-no-asp-net/6704
            System.Web.Caching.Cache dadosCache = HttpRuntime.Cache;
            if (dadosCache.Get("Utilizadores") == null)
            {
                dadosCache.Insert(ChavesMestre[0], DateTime.Now);
                utilizadores.Clear();
                utilizadores = UtilizadorService.ObterUtilizadores();
                System.Web.Caching.CacheDependency cd = new System.Web.Caching.CacheDependency(null, ChavesMestre);
                dadosCache.Insert("Utilizadores", utilizadores, cd);
            }
            else
            {
                utilizadores.Clear();
                utilizadores = dadosCache.Get("Utilizadores") as List<Utilizador>;
            }

            //// Faz Pesquisa
            string searchParam = Request.Params["search[value]"].ToLower();
            if (!string.IsNullOrEmpty(searchParam))
            {
                utilizadores = utilizadores.Where(x => x.nome.ToLower().Contains(searchParam)
                || x.idUtilizador.ToString().Contains(searchParam)
                || x.email.ToLower().Contains(searchParam)).ToList();
            }

            //// Faz Paginação

            if (Request.Params["order[0][dir]"] == "asc")
            {
                switch (Request.Params["order[0][column]"])
                {
                    case "0":
                        utilizadores = utilizadores.OrderBy(x => x.idUtilizador).ToList();
                        break;

                    case "1":
                        utilizadores = utilizadores.OrderBy(x => x.nome).ToList();
                        break;

                    case "2":
                        utilizadores = utilizadores.OrderBy(x => x.email).ToList();
                        break;

                    case "3":
                        utilizadores = utilizadores.OrderBy(x => x.tipoUtilizador).ToList();
                        break;

                    default:
                        break;
                }
            }
            else
            {
                switch (Request.Params["order[0][column]"])
                {
                    case "0":
                        utilizadores = utilizadores.OrderByDescending(x => x.idUtilizador).ToList();
                        break;
                    case "1":
                        utilizadores = utilizadores.OrderByDescending(x => x.nome).ToList();
                        break;

                    case "2":
                        utilizadores = utilizadores.OrderByDescending(x => x.email).ToList();
                        break;

                    case "3":
                        utilizadores = utilizadores.OrderByDescending(x => x.tipoUtilizador).ToList();
                        break;

                    default:
                        break;
                }
            }

            List<Utilizador> reducedlocalidades = utilizadores.Skip(int.Parse(Request.Params["start"])).Take(int.Parse(Request.Params["length"])).ToList();

            var chk = new
            {
                draw = Request.Params["draw"],
                //Data representa um array com as colunas (Ncolunas = quantidade de items devolvidos
                data = reducedlocalidades.Select(x =>
                    new string[] {
                        x.idUtilizador.ToString(),
                        x.nome.ToString(),
                        x.email.ToString(),
                        x.tipoUtilizador.ToString(),
                    }),
                recordsTotal = utilizadores.Count(),
                recordsFiltered = utilizadores.Count()
            };
            return JsonConvert.SerializeObject(chk);
        }
        /////
        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.Mensagem = mensagem;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Utilizador model, string returnUrl)
        {
            mensagem = "";
            // https://www.codeproject.com/articles/578374/aplusbeginner-splustutorialplusonpluscustomplusf

            UtilizadorService UtilizadorService = new UtilizadorService();

            if (!string.IsNullOrEmpty(model.password))
            {
                string hashedPwd = Criptografia.HashString(model.password);

                if (UtilizadorService.VerificaUtilizador(model.email, hashedPwd))
                {
                    Utilizador utilizador = UtilizadorService.ObterUtilizadorByEmail(model.email);
                    if (utilizador.idtipo != (int)TipoUtilizadorEnum.Cliente)
                    {
                        FuncionariosService funcionariosService = new FuncionariosService();

                        Funcionario funcionario = funcionariosService.ObterFuncionarioByIdUtilizador(utilizador.idUtilizador);

                        if (!funcionario.ativo)
                        {
                            ModelState.AddModelError("", "Utilizador Inactivo, contacte o Administrador");
                            return View(model);
                        }
                    }

                    FormsAuthentication.SetAuthCookie(model.email, false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Password ou utilizador incorrecto");
                }
            }
            else
            {
                ModelState.AddModelError("", "Password é necessária para poder autenticar");
            }
            //}

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Registo()
        {
            mensagem = "";

            ClientesService servico = new ClientesService();

            RegistoClienteViewModel cvm = new RegistoClienteViewModel();
            cvm.Cliente = servico.ObterClienteDefault();
            cvm.Utilizador = usrService.ObterUtilizadorDefault();

            cvm.Enderecos = new List<SelectListItem>() {
                new SelectListItem() { Text = "Introduza o seu código postal..." }
            };

            return View(cvm);
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registo(RegistoClienteViewModel vm, string returnUrl)
        {
            ClientesService servico = new ClientesService();
            if (servico.CreateCliente(vm.Cliente))
            {
                vm.Utilizador = new Utilizador()
                {
                    ativo = true,
                    idtipo = 3,
                    nrcontribuinte = vm.Cliente.nrcontribuinte,
                    email = vm.Cliente.email,
                    password = vm.Password,
                    nome = vm.Cliente.nome,
                    foto = "/images/profile_photo/default_img.jpg"
                };

                if (usrService.CreateUtilizador(vm.Utilizador))
                {
                    EmailService emailSvc = new EmailService();
                    var link = "http://" + Request.Url.Authority + Url.Action("ConfirmarRegisto", "Home", new { hash = Criptografia.HashString(vm.Utilizador.email) });

                    emailSvc.EnviarEmailRegisto(vm.Utilizador, link);

                    mensagem = "Foi lhe enviado um email de confirmação, por favor confira o seu email.";

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(vm);
        }

        private Utilizador ObterUtilizador()
        {
            string userName = ControllerContext.HttpContext.User.Identity.Name;
            UtilizadorService UtilizadorService = new UtilizadorService();

            Utilizador utilizador = UtilizadorService.ObterUtilizadorByEmail(userName);
            ViewBag.eAdmin = utilizador.idtipo == (int)TipoUtilizadorEnum.Administrador;

            return utilizador;

        }

        [AllowAnonymous]
        public ActionResult Perfil()
        {
            var user = ObterUtilizador();

            if (user.idtipo == (int)TipoUtilizadorEnum.Cliente)
            {
                return RedirectToAction("Edit", "Clientes", new { id = user.nrcontribuinte });
            }
            else
            {
                return RedirectToAction("EditByIdUtilizador", "Funcionarios", new { id = user.idUtilizador });
            }
        }

        [AllowAnonymous]
        public ActionResult RedefinirPass()
        {
            ConfirmRegistoViewModel viewModel = new ConfirmRegistoViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RedefinirPass(ConfirmRegistoViewModel vm)
        {
            EmailService emailSvc = new EmailService();

            UtilizadorService UtilizadorService = new UtilizadorService();
            Utilizador utilizador = UtilizadorService.ObterUtilizadorByEmail(vm.Username);

            var link = "http://" + Request.Url.Authority + Url.Action("ConfirmarRedefinirPass", "Utilizadores", new { hash = Criptografia.HashString(vm.Username) });

            emailSvc.EnviarEmailRecuperacao(utilizador, link);

            mensagem = "Foi lhe enviado um email de confirmação, por favor confira o seu email.";

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult ConfirmarRedefinirPass(string hash)
        {
            ConfirmRegistoViewModel viewModel = new ConfirmRegistoViewModel();
            viewModel.Hash = hash;
            return View(viewModel);
        }

        [HttpPost, AllowAnonymous]
        [ActionName("ConfirmarRedefinirPass")]
        public ActionResult ConfirmarRedefinirPass(ConfirmRegistoViewModel utilizador)
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

                    FormsAuthentication.SetAuthCookie(utilizador.Username, true);

                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ViewBag.Mensagem = "Passwords não coincidem";
                }
            }

            return View();
        }
    }
}
