using RescueDesk.Models;
using RescueDesk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.Controllers
{
    public class AtividadeController : Controller
    {
        // GET: Atividade
        public ActionResult Index()
        {
            AtividadeService atividadeService = new AtividadeService();

            return View(atividadeService.ObterAtividades());
        }

        // GET: Atividade/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Atividade/Create
        public ActionResult Create()
        {
            Atividade atividade = new Atividade();

            return View(atividade);
        }

        // POST: Atividade/Create
        [HttpPost]
        public ActionResult Create(Atividade atividade)
        {
            AtividadeService atividadeService = new AtividadeService();

            if (atividadeService.CreateAtividade(atividade))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(atividade);
            }
        }

        // GET: Atividade/Edit/5
        public ActionResult Edit(int id)
        {
            AtividadeService atividadeService = new AtividadeService();

            return View(atividadeService.ObterAtividade(id));
        }

        // POST: Atividade/Edit/5
        [HttpPost]
        public ActionResult Edit(Atividade atividade)
        {
            AtividadeService atividadeService = new AtividadeService();

            if (atividadeService.UpdateAtividade(atividade))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(atividade);
            }
        }

        // GET: Atividade/Delete/5
        public ActionResult Delete(int id)
        {
            AtividadeService atividadeService = new AtividadeService();

            return View(atividadeService.ObterAtividade(id));
        }

        // POST: Atividade/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int idatividade)
        {
            AtividadeService atividadeService = new AtividadeService();

            if (atividadeService.DeleteAtividade(idatividade))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Delete", new { id = idatividade });
            }
        }
    }
}
