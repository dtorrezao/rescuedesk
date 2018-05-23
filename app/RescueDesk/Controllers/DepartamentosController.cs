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
    public class DepartamentosController : Controller
    {
        // GET: Departamentos
        public ActionResult Index()
        {
            DepartamentosService servico = new DepartamentosService();

            return View(servico.ObterDepartamentos());
        }
        
        
        // GET: Departamentos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Departamentos/Create
        public ActionResult Create()
        {
            DepartamentosService servico = new DepartamentosService();

            return View(servico.ObterDepartamentoDefault());
        }

        // POST: Departamentos/Create
        [HttpPost]
        public ActionResult Create(Departamento departamento)
        {
            DepartamentosService servico = new DepartamentosService();
            if (servico.CreateDepartamento(departamento))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(departamento);
            }
        }

        // GET: Departamentos/Edit/5

        public ActionResult Edit(int id)
        {
            DepartamentosService servico = new DepartamentosService();

            return View(servico.ObterDepartamento(id));
        }

        // POST: Departamentos/Edit/5
        [HttpPost]
        public ActionResult Edit(Departamento departamento)
        {
            DepartamentosService servico = new DepartamentosService();
            if (servico.UpdateDepartamento(departamento))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(departamento);
            }
        }

        // GET: Departamentos/Delete/5
        public ActionResult Delete(int id)
        {
            DepartamentosService servico = new DepartamentosService();

            return View(servico.ObterDepartamento(id));
        }

        // POST: Localidades/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int iddept)
        {
            DepartamentosService servico = new DepartamentosService();
            if (servico.DeleteDepartamentos(iddept))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Delete", new { id = iddept });
            }
        }
    }
}
