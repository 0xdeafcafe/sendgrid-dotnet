using Newtonsoft.Json;

namespace SendGrid.Models.Mail
{
	public class Content
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("value")]
		public string Value { get; set; }
	}
}
