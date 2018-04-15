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
    }
}