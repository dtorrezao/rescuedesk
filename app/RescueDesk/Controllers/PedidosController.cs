using RescueDesk.Models;
using RescueDesk.Models.enums;
using RescueDesk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RescueDesk.Controllers
{
    [Authorize] // Todas as acções deste controlador estão disponiveis para os tipos de utlizador (apenas tem que fazer login)
    public class PedidosController : Controller
    {
        private Utilizador ObterUtilizador()
        {
            string userName = ControllerContext.HttpContext.User.Identity.Name;
            UtilizadorService UtilizadorService = new UtilizadorService();

            Utilizador utilizador = UtilizadorService.ObterUtilizadorByEmail(userName);
            ViewBag.eAdmin = utilizador.idtipo == (int)TipoUtilizadorEnum.Administrador;

            return utilizador;
        }

        // GET: Pedidos
        public ActionResult Index()
        {
            PedidosService servico = new PedidosService();

            return View("Index", servico.ObterPedidos(this.ObterUtilizador()));
        }

        public ActionResult ListarMeusPedidos()
        {
            PedidosService servico = new PedidosService();

            return View("Index", servico.ObterPedidos(this.ObterUtilizador(), true));
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult ListarPedidosPendentes()
        {
            PedidosService servico = new PedidosService();

            return View("Index", servico.ObterPedidosPendentes(this.ObterUtilizador()));
        }


        public ActionResult AtribuirFuncionario(int id)
        {
            PedidosService servico = new PedidosService();

            return View(servico.ObterPedido(id));
        }
        [HttpPost]
        public ActionResult AtribuirFuncionario(Pedido pedido)
        {
            PedidosService servico = new PedidosService();
            if (servico.UpdatePedido(pedido))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(pedido);
            }
        }

        public ActionResult Create()
        {
            PedidosService servico = new PedidosService();
            Pedido pedido = servico.ObterPedidoDefault();
            ClientesService clientes = new ClientesService();

            ViewBag.ListaClientes = this.ListaClientes(clientes);

            if (RescueDesk.Utils.ViewHelper.IsInGroup(new TipoUtilizadorEnum[] { TipoUtilizadorEnum.Cliente }))
            {
                Utilizador utilizador = ObterUtilizador();
                pedido.nrcontribuinte = utilizador.nrcontribuinte.Value;
            }

            return View(pedido);
        }

        [HttpPost]
        public ActionResult Create(Pedido pedido)
        {
            PedidosService servico = new PedidosService();
            if (servico.CreatePedido(pedido))
            {
                return this.RedirectToAction("ListarMeusPedidos");
            }
            else
            {
                return View(pedido);
            }
        }


        public ActionResult Encaminhar(int id)
        {
            PedidosService servico = new PedidosService();
            ServicosService servicosService = new ServicosService();
            FuncionariosService funcionarios = new FuncionariosService();
            Pedido pedido = servico.ObterPedido(id);
            ViewBag.TiposActividade = this.ListaTiposActividade(servicosService);

            ViewBag.ListaFuncionarios = this.ListaFuncionarios(funcionarios, pedido.idfuncionario.Value);

            return View(pedido);
        }

        private List<SelectListItem> ListaTiposActividade(ServicosService servico)
        {
            //listar moradas disponiveis
            var lista = new List<SelectListItem>();
            foreach (var item in servico.ObterServicos())
            {
                lista.Add(new SelectListItem() { Text = item.descricao, Value = item.idatividade.ToString() });
            }
            return lista;
        }

        private List<SelectListItem> ListaFuncionarios(FuncionariosService servico, int funcionarioId)
        {
            //listar moradas disponiveis
            var lista = new List<SelectListItem>();
            foreach (var item in servico.ObterFuncionarios().Where(x => x.ativo || x.idfuncionario == funcionarioId))
            {
                lista.Add(new SelectListItem() { Text = item.nome, Value = item.idfuncionario.ToString() });
            }
            return lista;
        }

        private List<SelectListItem> ListaClientes(ClientesService servico)
        {
            //listar moradas disponiveis
            var lista = new List<SelectListItem>();
            foreach (var item in servico.ObterClientes())
            {
                lista.Add(new SelectListItem() { Text = string.Format("{0} - {1}", item.nrcontribuinte, item.nome), Value = item.nrcontribuinte.ToString() });
            }
            return lista;
        }

        [HttpPost]
        public ActionResult Encaminhar(Pedido pedido)
        {
            PedidosService servico = new PedidosService();
            pedido.dtlido = DateTime.Now;
            if (servico.UpdatePedido(pedido))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(pedido);
            }
        }

        public ActionResult Edit(int id)
        {
            PedidosService servico = new PedidosService();
            ServicosService servicosService = new ServicosService();
            FuncionariosService funcionarios = new FuncionariosService();
            ClientesService clientes = new ClientesService();

            ViewBag.ListaClientes = this.ListaClientes(clientes);
            ViewBag.TiposActividade = this.ListaTiposActividade(servicosService);

            Pedido pedido = servico.ObterPedido(id);
            ViewBag.ListaFuncionarios = this.ListaFuncionarios(funcionarios, pedido.idfuncionario.Value);
            return View(pedido);
        }

        [HttpPost]
        public ActionResult Edit(Pedido pedido)
        {
            PedidosService servico = new PedidosService();


            if (servico.UpdatePedido(pedido))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(pedido);
            }
        }


        public ActionResult Delete(int id)
        {
            PedidosService servico = new PedidosService();

            return View(servico.ObterPedido(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int idpedido)
        {
            PedidosService servico = new PedidosService();
            if (servico.DeletePedido(idpedido))
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Delete", new { id = idpedido });
            }
        }


        public ActionResult Details(int id)
        {
            PedidosService servico = new PedidosService();
            ServicosService servicosService = new ServicosService();
            ClientesService clientes = new ClientesService();
            FuncionariosService funcionarios = new FuncionariosService();
            ViewBag.ListaClientes = this.ListaClientes(clientes);
            ViewBag.TiposActividade = this.ListaTiposActividade(servicosService);
            Pedido pedido = servico.ObterPedido(id);
            ViewBag.ListaFuncionarios = this.ListaFuncionarios(funcionarios, pedido.idfuncionario.Value);
            return View(pedido);
        }

    }

}