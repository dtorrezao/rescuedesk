using RescueDesk.Models;
using RescueDesk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.Controllers
{
    [Authorize(Roles = "Administrador")] // Todas as acções deste controlador estão disponiveis para os tipos de utlizador
    public class TipoUtilizadorController : Controller
    {
        // GET: TipoUtilizador
        public ActionResult Index()
        {
            TipoUtilizadorService TipoUserService = new TipoUtilizadorService();

            return View(TipoUserService.ObterTipos());
        }

        // GET: TipoUtilizador/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TipoUtilizador/Create
        public ActionResult Create()
        {
            TipoUtilizador tipo = new TipoUtilizador();
            return View(tipo);
        }

        // POST: Atividade/Create
        [HttpPost]
        public ActionResult Create(TipoUtilizador tipo)
        {
            TipoUtilizadorService TipoUserService = new TipoUtilizadorService();

            if (TipoUserService.CreateTipoUser(tipo))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(tipo);
            }
        }

        // POST: TipoUtilizador/Edit/5
        public ActionResult Edit(int id)
        {
            TipoUtilizadorService TipoUserService = new TipoUtilizadorService();

            return View(TipoUserService.ObterTipo(id));
        }

        // POST: Departamentos/Edit/5
        [HttpPost]
        public ActionResult Edit(TipoUtilizador tipo)
        {
            TipoUtilizadorService TipoUserService = new TipoUtilizadorService();
            if (TipoUserService.UpdateTipo(tipo))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(tipo);
            }
        }

        // GET: TipoUtilizador/Delete/5
        public ActionResult Delete(int id)
        {
            TipoUtilizadorService TipoUserService = new TipoUtilizadorService();

            return View(TipoUserService.ObterTipo(id));
        }

        // POST: Atividade/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int idtipo)
        {
            TipoUtilizadorService TipoUserService = new TipoUtilizadorService();

            if (TipoUserService.DeleteTipo(idtipo))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Delete", new { id = idtipo });
            }
        }
    }
}
