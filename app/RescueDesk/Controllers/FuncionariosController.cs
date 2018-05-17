using RescueDesk.Models;
using RescueDesk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.Controllers
{
    public class FuncionariosController : Controller
    {
        // GET: Funcionarios
        public ActionResult Index()
        {
            FuncionariosService servico = new FuncionariosService();

            return View(servico.ObterFuncionarios());
        }

        // GET: Funcionarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Funcionarios/Create
        public ActionResult Create()
        {
            Funcionario funcionario = new Funcionario();

            return View(funcionario);
        }

        // POST: Departamentos/Create
        [HttpPost]
        public ActionResult Create(Funcionario funcionario)
        {
            FuncionariosService servico = new FuncionariosService();
            if (servico.CreateFuncionario(funcionario))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(funcionario);
            }
        }

        // GET: Funcionarios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Funcionarios/Edit/5
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

        // GET: Funcionarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Funcionarios/Delete/5
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
