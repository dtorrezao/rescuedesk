using Newtonsoft.Json;
using RescueDesk.Models;
using RescueDesk.Models.enums;
using RescueDesk.Services;
using RescueDesk.Utils;
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
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Utilizador model, string returnUrl)
        {
            // https://www.codeproject.com/articles/578374/aplusbeginner-splustutorialplusonpluscustomplusf

            UtilizadorService UtilizadorService = new UtilizadorService();

            string hashedPwd = Criptografia.HashString(model.password);

            if (UtilizadorService.VerificaUtilizador(model.email, hashedPwd))
            {
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
            //}

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
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
    }
}
