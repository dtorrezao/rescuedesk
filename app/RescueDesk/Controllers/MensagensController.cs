using RescueDesk.Models;
using RescueDesk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.Controllers
{
    public class MensagensController : Controller
    {
        private Utilizador ObterUtilizador()
        {
            string userName = ControllerContext.HttpContext.User.Identity.Name;
            UtilizadorService UtilizadorService = new UtilizadorService();
            return UtilizadorService.ObterUtilizadorByEmail(userName);      
        }

        // GET: Mensagens
        public ActionResult Index()
        {
            MensagensService servico = new MensagensService();

            return View(servico.ObterMensagens(this.ObterUtilizador(), true, true));
        }

        // GET: Mensagens
        public ActionResult Enviadas()
        {
            MensagensService servico = new MensagensService();
            ViewBag.mensagem = 1;                 
            return View("Index", servico.ObterMensagens(this.ObterUtilizador(), true, false));
        }

        public ActionResult Inbox()
        {
            MensagensService servico = new MensagensService();
            ViewBag.mensagem = 2;
            return View("Index", servico.ObterMensagens(this.ObterUtilizador(), false, true));
        }

        // GET: Mensagens/Details/5
        public ActionResult Details(int id)
        {
            MensagensService servico = new MensagensService();
            Mensagem msg = servico.ObterMensagem(id);
            UtilizadorService service = new UtilizadorService();

            ViewBag.ListaUtilizadores = this.ListaUtilizadores(service);

            msg.lido = true;
            servico.UpdateMensagem(msg,msg.lido);

            Utilizador utilizador = this.ObterUtilizador();

            if (msg.emissor == utilizador.idUtilizador)
            {
                ViewBag.vmensagem = 1;
            }
            else
            {
                ViewBag.vmensagem = 2;
            }

            return View(msg);
        }

        // GET: Mensagens/Create
        public ActionResult Create()
        {
            MensagensService servico = new MensagensService();
            UtilizadorService service = new UtilizadorService();
            ViewBag.ListaUtilizadores = this.ListaUtilizadores(service);
            return PartialView(new Mensagem() { });
        }

        private List<SelectListItem> ListaUtilizadores(UtilizadorService servico)
        {
            //listar moradas disponiveis
            var lista = new List<SelectListItem>();
            foreach (var item in servico.ObterUtilizadores())
            {
                lista.Add(new SelectListItem() { Text = string.Format("{0} - {1}", item.nome, item.email), Value = item.idUtilizador.ToString() });
            }
            return lista;
        }

        // POST: Mensagens/Create
        [HttpPost]
        public ActionResult Create(Mensagem mensagem)
        {
            MensagensService servico = new MensagensService();
            mensagem.dtenviado = DateTime.Now;
            mensagem.emissor = this.ObterUtilizador().idUtilizador;

            if (servico.CreateMensagem(mensagem, this.ObterUtilizador()))
            {
                return this.RedirectToAction("Inbox");
            }
            else
            {
                return View(mensagem);
            }
        }
    }
}
