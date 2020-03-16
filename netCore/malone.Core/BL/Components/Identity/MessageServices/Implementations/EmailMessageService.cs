using malone.Core.BL.Components.Identity.MessageServices.Interfaces;
using malone.Core.EL.Identity;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace malone.Core.BL.Components.Identity.MessageServices
{

    public class EmailService : IEmailMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            if (message is null) throw new ArgumentNullException(nameof(message));

            using (SmtpClient client = new SmtpClient())
            {
                using (MailMessage mailCliente = new MailMessage())
                {
                    client.SendCompleted += (s, e) => client.Dispose();

                    mailCliente.To.Add(message.Destination);
                    mailCliente.Subject = message.Subject;
                    mailCliente.Body = message.Body;
                    mailCliente.IsBodyHtml = true;

                    await client.SendMailAsync(mailCliente).ConfigureAwait(false);
                }
            }
        }
    }

}
