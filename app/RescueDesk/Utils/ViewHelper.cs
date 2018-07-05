using RescueDesk.Models;
using RescueDesk.Models.enums;
using RescueDesk.Services;
using RescueDesk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RescueDesk.Utils
{
    public static class ViewHelper
    {
        public static string PageAtiva = "";

        public static int ObterContagemPedidosPendentes()
        {
            string userName = HttpContext.Current.User.Identity.Name;
            UtilizadorService UtilizadorService = new UtilizadorService();
            Utilizador utilizador = UtilizadorService.ObterUtilizadorByEmail(userName);
            PedidosService servico = new PedidosService();
            var pedidos = servico.ObterPedidosPendentes(utilizador);
            return pedidos.Count();
        }

        public static int ObterContagemMensagens()
        {
            string userName = HttpContext.Current.User.Identity.Name;
            MensagensService service = new MensagensService();
            UtilizadorService UtilizadorService = new UtilizadorService();
            Utilizador utilizador = UtilizadorService.ObterUtilizadorByEmail(userName);
            return service.ObterMensagens(utilizador, false, true).Where(x => !x.lido).Count();
        }

        public static int ObterContagemNotas()
        {
            string userName = HttpContext.Current.User.Identity.Name;
            NotasService service = new NotasService();
            UtilizadorService UtilizadorService = new UtilizadorService();
            Utilizador utilizador = UtilizadorService.ObterUtilizadorByEmail(userName);
            return service.ObterNotas(utilizador).Count();
        }

        public static int ObterContagemMeusPedidos()
        {
            string userName = HttpContext.Current.User.Identity.Name;
            UtilizadorService UtilizadorService = new UtilizadorService();
            Utilizador utilizador = UtilizadorService.ObterUtilizadorByEmail(userName);
            PedidosService servico = new PedidosService();
            var pedidos = servico.ObterPedidos(utilizador, true);
            return pedidos.Count();
        }

        public static List<MensagemViewModel> ObterMensagens(int qtdMsg)
        {
            var list = new List<MensagemViewModel>();

            string userName = HttpContext.Current.User.Identity.Name;
            MensagensService service = new MensagensService();
            UtilizadorService UtilizadorService = new UtilizadorService();
            Utilizador utilizador = UtilizadorService.ObterUtilizadorByEmail(userName);
            var mensagens = service.ObterMensagens(utilizador, false, true).Where(x => !x.lido).Take(qtdMsg);


            foreach (var item in mensagens)
            {
                string assunto = item.assunto.Length < 20 ? item.assunto : item.assunto.Substring(0, 20) + "...";
                list.Add(new MensagemViewModel()
                {
                    Utilizador = UtilizadorService.ObterUtilizador(item.emissor),
                    Assunto = assunto,
                    DataEnviada = item.dtenviado,
                    Link = item.idmensagem.ToString()
                });
            }

            return list;
        }
        public static bool IsInGroup(TipoUtilizadorEnum[] tiposAutorizados)
        {
            string userName = HttpContext.Current.User.Identity.Name;
            UtilizadorService UtilizadorService = new UtilizadorService();

            Utilizador utilizador = UtilizadorService.ObterUtilizadorByEmail(userName);

            return tiposAutorizados.Any(x => (int)x == utilizador.idtipo);
        }

        public static bool IsAdmin()
        {
            return ViewHelper.IsInGroup(new TipoUtilizadorEnum[] { TipoUtilizadorEnum.Administrador });
        }

        public static bool IsFuncionario()
        {
            return ViewHelper.IsInGroup(new TipoUtilizadorEnum[] { TipoUtilizadorEnum.Funcionário });
        }

        public static bool IsCliente()
        {
            return ViewHelper.IsInGroup(new TipoUtilizadorEnum[] { TipoUtilizadorEnum.Cliente });
        }


        public static string IsActivePage(string actionName, string controllerName)
        {
            //https://stackoverflow.com/questions/18248547/get-controller-and-action-name-from-within-controller
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
            string controller = "", action = "";

            if (routeValues.ContainsKey("controller"))
                controller = (string)routeValues["controller"];

            if (routeValues.ContainsKey("action"))
                action = (string)routeValues["action"];

            return controller == controllerName && action == actionName ? "active" : "";
        }

        public static string GetUserImage()
        {
            var username = HttpContext.Current.User.Identity.Name;

            UtilizadorService usrService = new UtilizadorService();
            Utilizador user = usrService.ObterUtilizadorByEmail(username);
            return user.foto ?? "/images/admin.jpg";
        }

        static string userName = HttpContext.Current.User.Identity.Name;
        static UtilizadorService UtilizadorService = new UtilizadorService();
        static Utilizador utilizador = UtilizadorService.ObterUtilizadorByEmail(userName);
        
        static MensagensService mensagensService = new MensagensService();
        public static int MensagensContagem = (mensagensService.ObterMensagens(utilizador, false, true).Count());

        static ClientesService clientesService = new ClientesService();
        public static int ClientesContagem = clientesService.ObterClientes().Count();

        static DepartamentosService departamentosService = new DepartamentosService();
        public static int DepartamentosContagem = departamentosService.ObterDepartamentos().Count();

        static FuncionariosService funcionariosService = new FuncionariosService();
        public static int FuncionariosContagem = funcionariosService.ObterFuncionarios().Count();

        static AddressService addressService = new AddressService();
        public static int LocalidadesContagem = addressService.ObterLocalidades().Count();

        static PedidosService pedidosService = new PedidosService();
        public static int PedidosContagem = pedidosService.ObterPedidos(utilizador).Count();

        public static int UtilizadoresContagem = UtilizadorService.ObterUtilizadores().Count();

        static ServicosService servicosService = new ServicosService();
        public static int ServicosContagem = servicosService.ObterServicos().Count();
    }
}