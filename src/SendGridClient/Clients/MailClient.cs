using System.Threading.Tasks;
using SendGrid.Abstracts;
using SendGrid.Connections;
using SendGrid.Interfaces;
using SendGrid.Models.Mail;

namespace SendGrids
{
	public class MailClient
		: ApiClient, IMailClient
	{
		internal MailClient(ApiKeyConnection connection)
			: base(connection)
		{ }

		public async Task SendAsync(Email email)
		{
			await HttpConnection.PostAsync<object, Email>("mail/send", email);
		}
	}
}
