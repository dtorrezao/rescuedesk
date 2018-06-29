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
            DashboardService service = new DashboardService();
            vm.PedidosPorMes = service.ObterPedidosPorMes();
            vm.ClienteMaisPedidos = service.ObterClienteMaisPedidos();
            vm.ServicoMaisPedidos = service.ObterServicoMaisPedidos();
            vm.FuncionarioMaisPedidos = service.ObterFuncionarioMaisPedidos();

            vm.ProfileCard = new ProfileCardViewModel();
            vm.ProfileCard.Nome = "David";
            vm.ProfileCard.Posicao = "Programador";
            vm.ProfileCard.Foto = "/images/admin.jpg";

            PedidosService servico = new PedidosService();
            vm.PedidosTop5 = servico.ObterPedidosPendentes(this.ObterUtilizador()).Take(5).ToList();
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
                backgroundColor = ObterCor(x),
                textColor = ObterTextColor(x)
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
                    cor = "#ff4e00";
                    break;
                case Models.enums.prioridade.Baixa:
                    cor = "#3c8ada";
                    break;
                case Models.enums.prioridade.Critica:
                    cor = "#b60000";
                    break;
                case Models.enums.prioridade.Media:
                    cor = "#fff600";
                    break;
                default:
                    cor = "#FFFFFF";
                    break;
            }

            return cor;
        }

        private string ObterTextColor(Pedido pedidop)
        {
            string cor;
            //url = "http://www.google.com", backgroundColor = "#378006"
            switch (pedidop.prioridade)
            {
                case Models.enums.prioridade.Alta:
                    cor = "#FFFFFF";
                    break;
                case Models.enums.prioridade.Baixa:
                    cor = "#FFFFFF";
                    break;
                case Models.enums.prioridade.Critica:
                    cor = "#FFFFFF";
                    break;
                case Models.enums.prioridade.Media:
                    cor = "#000000";
                    break;
                default:
                    cor = "#000000";
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