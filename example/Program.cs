using System.Collections.Generic;
using SendGrid;
using SendGrid.Connections;
using SendGrid.Models.Mail;

namespace ConsoleApplication
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var key = new ApiKeyConnection("SG.api.key");
			var client = new SendGridClient(key);
			client.MailClient.SendAsync(new Email
			{
				Personalizations = new List<Personalization>
				{
					new Personalization
					{
						To = new List<EmailDetail>
						{
							new EmailDetail
							{
								Email = "sendgrid@alx.red",
								Name = "Alex Forbes-Reed"
							}
						}
					}
				},
				From = new EmailDetail
				{
					Email = "test@helm.global",
					Name = "Helm Test"
				},
				Subject = "Test Email",
				Content = new List<Content>
				{
					new Content
					{
						Type = "text/html",
						Value = "<h1>Test Email :)</h1>"
					}
				}
			}).Wait();
		}
	}
}
