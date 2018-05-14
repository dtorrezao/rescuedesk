﻿using RescueDesk.Models;
using RescueDesk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.Controllers
{
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
    }
}
