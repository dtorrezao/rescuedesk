﻿using RescueDesk.Models;
using RescueDesk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.Controllers
{
    [Authorize] // Todas as acções deste controlador estão disponiveis para os tipos de utlizador (apenas tem que fazer login)
    public class NotasController : Controller
    {
        // GET: Notas
        public ActionResult Index()
        {
            NotasService servico = new NotasService();

            return View(servico.ObterNotas());
        }
        
        // GET: Notas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Notas/Create
        public ActionResult Create()
        {
            NotasService servico = new NotasService();

            return PartialView(new Notas() { });
        }

        // POST: Notas/Create
        [HttpPost]
        public ActionResult Create(Notas nota)
        {
            NotasService servico = new NotasService();
            if (servico.CreateNota(nota))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(nota);
            }
        }

        // GET: Notas/Edit/5

        public ActionResult Edit(int id)
        {
            NotasService servico = new NotasService();

            return PartialView(servico.ObterNota(id));
        }

        // POST: Notas/Edit/5
        [HttpPost]
        public ActionResult Edit(Notas nota)
        {
            NotasService servico = new NotasService();
            if (servico.UpdateNota(nota))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(nota);
            }
        }

        // GET: Notas/Delete/5
        public ActionResult Delete(int id)
        {
            NotasService servico = new NotasService();

            return PartialView(servico.ObterNota(id));

        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int idnota)
        {
            NotasService servico = new NotasService();
            if (servico.DeleteNota(idnota))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Delete", new { id = idnota });
            }
        }
    }
}
