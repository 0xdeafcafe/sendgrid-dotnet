using SendGrid.Interfaces;

namespace SendGrid
{
	public interface ISendGridClient
	{
		IMailClient MailClient { get; }
	}
}
