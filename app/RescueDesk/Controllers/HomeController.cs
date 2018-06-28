using Newtonsoft.Json;
using RescueDesk.Models;
using RescueDesk.Services;
using RescueDesk.Utils;
using RescueDesk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RescueDesk.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DashboardViewModel vm = new DashboardViewModel();

            vm.PedidosPorMes = 1000;
            vm.PedidosPorMesData = new int[] { 50, 59, 84, 84, 51, 55, 40 };
            vm.PedidosPorMesLabels = new string[] { "January", "February", "March", "April", "May", "June", "July" };

            vm.ClienteMaisPedidos = "1000";
            vm.ClienteMaisPedidosData = new int[] { 50, 59, 84, 84, 51, 55, 40 };
            vm.ClienteMaisPedidosLabels = new string[] { "January", "February", "March", "April", "May", "June", "July" };

            vm.ServicoMaisPedidos = "1000";
            vm.ServicoMaisPedidosData = new int[] { 50, 59, 84, 84, 51, 55, 40 };
            vm.ServicoMaisPedidosLabels = new string[] { "January", "February", "March", "April", "May", "June", "July" };

            vm.FuncionarioMaisPedidos = "1000";
            vm.FuncionarioMaisPedidosData = new int[] { 50, 59, 84, 84, 51, 55, 40 };
            vm.FuncionarioMaisPedidosLabels = new string[] { "January", "February", "March", "April", "May", "June", "July" };

            vm.ProfileCard = new ProfileCardViewModel();
            vm.ProfileCard.Nome = "Pedro";
            vm.ProfileCard.Posicao = "Programador";
            vm.ProfileCard.Foto = "/images/admin.jpg";

            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Calendario()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ConfirmarRegisto(string hash)
        {
            ConfirmRegistoViewModel viewModel = new ConfirmRegistoViewModel();
            viewModel.Hash = hash;
            return View(viewModel);
        }

        [HttpPost, AllowAnonymous]
        [ActionName("ConfirmarRegisto")]
        public ActionResult ConfirmarRegistoA(ConfirmRegistoViewModel utilizador)
        {
            string hashedUser = Criptografia.HashString(utilizador.Username);
            if (utilizador.Hash == hashedUser)
            {
                if (utilizador.Password == utilizador.ConfirmPassword)
                {
                    UtilizadorService usrService = new UtilizadorService();
                    var user = usrService.ObterUtilizadorByEmail(utilizador.Username);
                    user.password = utilizador.Password;
                    usrService.ChangePassword(user);

                    FormsAuthentication.SetAuthCookie(utilizador.Username, true);

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Mensagem = "Passwords não coincidem";
                }
            }

            return View();
        }

        public string GetEvents(string start, string end)
        {
            var pedidosservice = new PedidosService();

            var pedidop = pedidosservice.ObterPedidos(this.ObterUtilizador(), true, true);
            var servicos = new ServicosService();
            var tiposServico = servicos.ObterServicos().ToDictionary(x => x.idatividade);

            List<Evento> eventos = pedidop.Where(x => x.dtmarcado != null).Select(x => new Evento()
            {
                title = string.Format("[#{0}] - {1}", x.idpedido, x.assunto.ToString()),
                start = x.dtmarcado.Value,
                end = CalcularPeso(x.dtmarcado.Value, tiposServico[x.idatividade.Value]),
                backgroundColor = ObterCor(x)
            }).ToList();
            return JsonConvert.SerializeObject(eventos);
        }

        private string ObterCor(Pedido pedidop)
        {
            string cor;
            //url = "http://www.google.com", backgroundColor = "#378006"
            switch (pedidop.prioridade)
            {
                case Models.enums.prioridade.Alta:
                    cor = "#ffa500";
                    break;
                case Models.enums.prioridade.Baixa:
                    cor = "#FFFF00";
                    break;
                case Models.enums.prioridade.Critica:
                    cor = "#FF0000";
                    break;
                case Models.enums.prioridade.Media:
                    cor = "#016ca1";
                    break;
                default:
                    cor = "#FFFFFF";
                    break;
            }

            return cor;
        }

        private DateTime CalcularPeso(DateTime value, Servico servico)
        {
            return value.Date.AddMinutes(servico.peso * 10);
        }

        private Utilizador ObterUtilizador()
        {
            string userName = ControllerContext.HttpContext.User.Identity.Name;
            UtilizadorService UtilizadorService = new UtilizadorService();
            return UtilizadorService.ObterUtilizadorByEmail(userName);
        }
    }
}