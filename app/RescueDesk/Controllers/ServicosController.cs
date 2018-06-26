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
    public class ServicosController : Controller
    {
        // GET: Atividade
        public ActionResult Index()
        {
            ServicosService atividadeService = new ServicosService();

            return View(atividadeService.ObterServicos());
        }

        // GET: Atividade/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Atividade/Create
        public ActionResult Create()
        {
            Servico atividade = new Servico();

            return PartialView(atividade);
        }

        // POST: Atividade/Create
        [HttpPost]
        public ActionResult Create(Servico atividade)
        {
            ServicosService atividadeService = new ServicosService();

            if (atividadeService.CreateServico(atividade))
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
            ServicosService atividadeService = new ServicosService();

            return PartialView(atividadeService.ObterServico(id));
        }

        // POST: Atividade/Edit/5
        [HttpPost]
        public ActionResult Edit(Servico atividade)
        {
            ServicosService atividadeService = new ServicosService();

            if (atividadeService.UpdateServico(atividade))
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
            ServicosService atividadeService = new ServicosService();
            return PartialView(atividadeService.ObterServico(id));
        }

        // POST: Atividade/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int idatividade)
        {
            ServicosService atividadeService = new ServicosService();

            if (atividadeService.DeleteServico(idatividade))
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
