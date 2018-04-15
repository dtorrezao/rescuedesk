﻿using RescueDesk.Models;
using RescueDesk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.Controllers
{
    public class LocalidadesController : Controller
    {
        // GET: Localidades
        public ActionResult Index()
        {
            AddressService addressService = new AddressService();

            return View(addressService.ObterLocalidades());
        }

        // GET: Localidades/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Localidades/Create
        public ActionResult Create()
        {
            Localidade localidade = new Localidade();

            return View(localidade);
        }

        // POST: Localidades/Create
        [HttpPost]
        public ActionResult Create(Localidade localidade)
        {
            AddressService addressService = new AddressService();
            if (addressService.CreateLocalidade(localidade))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(localidade);
            }
        }

        // GET: Localidades/Edit/5
        public ActionResult Edit(string id)
        {
            AddressService addressService = new AddressService();

            return View(addressService.ObterLocalidade(id));
        }

        // POST: Localidades/Edit/5
        [HttpPost]
        public ActionResult Edit(Localidade localidade, string oldID)
        {
            AddressService addressService = new AddressService();
            if (addressService.UpdateLocalidade(localidade, oldID))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(localidade);
            }
        }

        // GET: Localidades/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Localidades/Delete/5
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
