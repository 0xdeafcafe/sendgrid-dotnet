using System.Threading.Tasks;

namespace SendGrid.Interfaces
{
	public interface IMailClient
	{
		Task SendAsync(string to, string toName, string subject, string htmlBody, string textBody, string from, string fromName);
	}
}
