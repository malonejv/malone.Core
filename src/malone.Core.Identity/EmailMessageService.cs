using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace malone.Core.Identity
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
