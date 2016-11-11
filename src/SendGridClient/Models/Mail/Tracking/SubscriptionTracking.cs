using Newtonsoft.Json;

namespace SendGrid.Models.Mail.Tracking
{
	public class SubscriptionTracking
	{
		[JsonProperty("enable")]
		public bool Enable { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("html")]
		public string Html { get; set; }

		[JsonProperty("substitution_tag")]
		public string SubstitutionTag { get; set; }
	}
}
