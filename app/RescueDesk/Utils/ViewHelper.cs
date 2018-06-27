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
        public static int ObterContagemMensagens()
        {
            string userName = HttpContext.Current.User.Identity.Name;
            MensagensService service = new MensagensService();
            UtilizadorService UtilizadorService = new UtilizadorService();
            Utilizador utilizador = UtilizadorService.ObterUtilizadorByEmail(userName);
            return service.ObterMensagens(utilizador, false, true).Where(x => !x.lido).Count();
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
                list.Add(new MensagemViewModel()
                {
                    Utilizador = UtilizadorService.ObterUtilizador(item.emissor),
                    Corpo = item.corpo.Substring(0, 20) + "...",
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
    }
}