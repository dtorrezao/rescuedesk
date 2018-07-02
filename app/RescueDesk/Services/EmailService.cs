using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using RescueDesk.Models;

namespace RescueDesk.Services
{
    public class EmailService
    {
        public void EnviarEmail(MailMessage mail)
        {
            var fromAddress = new MailAddress("rescuedesk.aesm@gmail.com", "RescueDesk");
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "rescuedesk#18")
            };
            mail.From = fromAddress;
            smtp.Send(mail);
        }

        public void EnviarEmailRegisto(Utilizador utilizador, string link)
        {
            var mail = new MailMessage();

            string body = string.Format("Olá {0}! Bem vindo ao RescueDesk. \r\n Clique aqui ({1}) para confirmar a sua conta.", utilizador.nome, link);

            mail.Body = body;
            mail.Subject = "RescueDesk - Novo Registo";
            mail.To.Add(new MailAddress(utilizador.email));

            EmailService emailSvc = new EmailService();
            emailSvc.EnviarEmail(mail);
        }

        public void EnviarEmailRecuperacao(Utilizador utilizador, string link)
        {
            var mail = new MailMessage();

            // TODO: mudar
            string body = string.Format("Olá {0}! Bem vindo ao RescueDesk. \r\n Clique aqui ({1}) para alterar a sua password.", utilizador.email, link);

            mail.Body = body;
            mail.Subject = "RescueDesk - Recuperar Senha";
            mail.To.Add(new MailAddress(utilizador.email));

            EmailService emailSvc = new EmailService();
            emailSvc.EnviarEmail(mail);
        }
    }
}