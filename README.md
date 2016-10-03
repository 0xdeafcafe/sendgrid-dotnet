sendgrid-dotnet
===

Little library for using SendGrid's v3 mail API with DotNet Core.

#### Simple Usage Example
``` csharp
var connection = new ApiKeyConnection("sendgrid api key");
var client = new SendGridClient(connection);
await client.MailClient.SendAsync("alex@github.com", "Alex Forbes-Reed", "Test Subject", "<h1>body</h1>", "body", "info@github.com", "Github Account");
```

#### Dependency Injection Example - thanks [@NotMyself](https://github.com/NotMyself)
``` csharp
public class ContactUsOptions
{
    public string ApiKey { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Subject { get; } = "Kiehl NW Website Contact Form Submission";
}

public class ContactUsService
{
    private readonly ContactUsOptions options;
    private readonly ILogger<ContactUsService> logger;

    public ContactUsService(IOptions<ContactUsOptions> accessor,
        ILogger<ContactUsService> logger)
    {
        this.logger = logger;
        this.options = accessor.Value;
    }

    public Task SendAsync(ContactViewModel vm)
    {
        logger?.LogInformation($"Sending Contact Us Message From {vm.Email}");

        return GetClient()?.SendAsync(
                 from: vm.Email
                , fromName: vm.Name
                , to: options.Email
                , toName: options.Name
                , subject: options.Subject
                , htmlBody: vm.Message
                , textBody: vm.Message);
    }

    private IMailClient GetClient()
    {
        if (string.IsNullOrWhiteSpace(options.ApiKey))
        {
            logger?.LogError("API Key Not Found: Unable to create SendGrid Client");
            return null;
        }

        var key = new ApiKeyConnection(options.ApiKey);
        var client = new SendGridClient(key);
        return client.MailClient;
    }
}

public void ConfigureServices(IServiceCollection services)
{
    services.Configure<ContactUsOptions>(o =>
    {
        o.ApiKey = Configuration["SENDGRID_API_KEY"];
        o.Name = Configuration["ContactUs:Name"];
        o.Email = Configuration["ContactUs:Email"];
    });

    services.AddScoped<ContactUsService>();

    services.AddMvc();
}
```
