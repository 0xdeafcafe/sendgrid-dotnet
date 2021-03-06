﻿using System.Collections.Generic;
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
								Email = "customer@yahoooooo.com",
								Name = "Customer Name"
							}
						}
					}
				},
				From = new EmailDetail
				{
					Email = "no-reply@company.domain",
					Name = "Company Letter"
				},
				Subject = "sup",
				Content = new List<Content>
				{
					new Content
					{
						Type = "text/html",
						Value = "<h1>yo yo! :)</h1>"
					}
				}
			}).Wait();
		}
	}
}
