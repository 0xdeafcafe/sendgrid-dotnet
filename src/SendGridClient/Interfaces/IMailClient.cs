using System.Threading.Tasks;
using SendGrid.Models.Mail;

namespace SendGrid.Interfaces
{
	public interface IMailClient
	{
		Task SendAsync(Email email);
	}
}
