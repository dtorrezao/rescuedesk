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

            mail.Body = "Bla Bla Bla " + link + " bla bla bla";
            mail.Subject = "Register";
            mail.To.Add(new MailAddress(utilizador.email));

            EmailService emailSvc = new EmailService();
            emailSvc.EnviarEmail(mail);
        }
    }
}