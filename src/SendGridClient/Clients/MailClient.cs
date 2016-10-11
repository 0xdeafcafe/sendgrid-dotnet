using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SendGrid.Abstracts;
using SendGrid.Connections;
using SendGrid.Interfaces;
using SendGrid.Models;

namespace SendGrids
{
	public class MailClient
		: ApiClient, IMailClient
	{
		internal MailClient(ApiKeyConnection connection)
			: base(connection)
		{ }

		public async Task SendAsync(string to, string toName, string subject, string htmlBody, string textBody, string from, string fromName)
		{
			var body = new Dictionary<string, string>
			{
				{ "to", to },
				{ "toname", toName },
				{ "subject", subject },
				{ "html", htmlBody },
				{ "text", textBody },
				{ "from", from },
				{ "fromname", fromName }
			};

			await HttpConnection.PostAsync<GenericResponse>("mail.send", body);
		}
	}
}
