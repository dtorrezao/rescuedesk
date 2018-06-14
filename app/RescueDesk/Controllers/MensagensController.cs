using RescueDesk.Models;
using RescueDesk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.Controllers
{
    public class MensagensController : Controller
    {
        private Utilizador ObterUtilizador()
        {
            string userName = ControllerContext.HttpContext.User.Identity.Name;
            UtilizadorService UtilizadorService = new UtilizadorService();
            return UtilizadorService.ObterUtilizadorByEmail(userName);
        }

        // GET: Mensagens
        public ActionResult Index()
        {
            MensagensService servico = new MensagensService();

            return View(servico.ObterMensagens(this.ObterUtilizador()));
        }

        // GET: Mensagens/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Mensagens/Create
        public ActionResult Create()
        {
            MensagensService servico = new MensagensService();

            return PartialView(new Mensagem() { });
        }

        // POST: Mensagens/Create
        [HttpPost]
        public ActionResult Create(Mensagem mensagem)
        {
            MensagensService servico = new MensagensService();
            if (servico.CreateMensagem(mensagem, this.ObterUtilizador()))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(mensagem);
            }
        }

        // GET: Mensagens/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Mensagens/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Mensagens/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Mensagens/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
