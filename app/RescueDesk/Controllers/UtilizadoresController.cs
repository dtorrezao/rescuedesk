using RescueDesk.Models;
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
            UtilizadorService UtilizadorService = new UtilizadorService();

            return View(UtilizadorService.ObterUtilizadores());
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
    }
}
