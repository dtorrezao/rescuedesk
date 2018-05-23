using RescueDesk.Models;
using RescueDesk.Services;
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

        // GET: Utilizadores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Utilizadores/Create
        public ActionResult Create()
        {
            Utilizador user = new Utilizador();
            return View(user);
        }

        // POST: Atividade/Create
        [HttpPost]
        public ActionResult Create(Utilizador user)
        {
            UtilizadorService UtilizadorService = new UtilizadorService();

            if (UtilizadorService.CreateUtilizador(user))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }

        //GET: Utilizadores/Edit/5
        public ActionResult Edit(string id)
        {
            UtilizadorService UtilizadorService = new UtilizadorService();

            return View(UtilizadorService.ObterUtilizador(id));
        }

        // POST: Atividade/Edit/5
        [HttpPost]
        public ActionResult Edit(Utilizador user)
        {
            UtilizadorService UtilizadorService = new UtilizadorService();

            if (UtilizadorService.UpdateUtilizador(user))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }

        // GET: Atividade/Delete/5
        public ActionResult Delete(string id)
        {
            UtilizadorService UtilizadorService = new UtilizadorService();

            return View(UtilizadorService.ObterUtilizador(id));
        }

        // POST: Atividade/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string email)
        {
            UtilizadorService UtilizadorService = new UtilizadorService();

            if (UtilizadorService.DeleteUtilizador(email))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Delete", new { id = email });
            }
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
            if (UtilizadorService.VerificaUtilizador(model.email, model.password))
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
