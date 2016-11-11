using Newtonsoft.Json;

namespace SendGrid.Models.Mail
{
	public class EmailDetail
	{
		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
