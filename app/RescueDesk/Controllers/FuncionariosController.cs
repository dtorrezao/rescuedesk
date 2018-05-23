using RescueDesk.Models;
using RescueDesk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.Controllers
{
    //[Authorize(Roles = "Administrador")]
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
            FuncionariosService servico = new FuncionariosService();


            return View(servico.ObterFuncionarioDefault());
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

        public ActionResult Edit(int id)
        {
            FuncionariosService servico = new FuncionariosService();

            return View(servico.ObterFuncionario(id));
        }

        [HttpPost]
        public ActionResult Edit(Funcionario funcionario)
        {
            FuncionariosService servico = new FuncionariosService();
            if (servico.UpdateFuncionario(funcionario))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(funcionario);
            }
        }

        public ActionResult Delete(int id)
        {
            FuncionariosService servico = new FuncionariosService();

            return View(servico.ObterFuncionario(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int idfuncionario)
        {
            FuncionariosService servico = new FuncionariosService();
            if (servico.DeleteFuncionario(idfuncionario))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Delete", new { id = idfuncionario });
            }
        }
    }
}
