sendgrid-dotnet
===

Little library for using SendGrid's v3 mail API with DotNet Core.

#### Simple Usage Example
``` csharp
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
```
