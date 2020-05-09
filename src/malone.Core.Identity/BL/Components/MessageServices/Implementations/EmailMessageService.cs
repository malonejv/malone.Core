using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using malone.Core.Identity.BL.Components.MessageServices.Interfaces;
using System.Net.Mail;

namespace malone.Core.Identity.BL.Components.MessageServices
{

    public class EmailService : IEmailMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var client = new SmtpClient();

            MailMessage mailCliente = new MailMessage();
            client.SendCompleted += (s, e) => client.Dispose();

            mailCliente.To.Add(message.Destination);
            mailCliente.Subject = message.Subject;
            mailCliente.Body = message.Body;
            mailCliente.IsBodyHtml = true;

            await client.SendMailAsync(mailCliente);
        }
    }

}
