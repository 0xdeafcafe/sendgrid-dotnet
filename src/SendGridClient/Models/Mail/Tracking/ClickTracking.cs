using Newtonsoft.Json;

namespace SendGrid.Models.Mail.Tracking
{
	public class ClickTracking
	{
		[JsonProperty("enable")]
		public bool Enable { get; set; }

		[JsonProperty("enable_text")]
		public bool EnableText { get; set; }
	}
}
