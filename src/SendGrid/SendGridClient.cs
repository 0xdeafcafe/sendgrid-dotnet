using SendGrid.Abstracts;
using SendGrid.Connections;
using SendGrid.Interfaces;
using SendGrids;

namespace SendGrid
{
	public class SendGridClient
		: ApiClient, ISendGridClient
	{
		protected SendGridClient(ApiKeyConnection connection)
			: base(connection)
		{
			MailClient = new MailClient(connection);
		}

		public IMailClient MailClient { get; }
	}
}
