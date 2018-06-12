using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

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
    }
}