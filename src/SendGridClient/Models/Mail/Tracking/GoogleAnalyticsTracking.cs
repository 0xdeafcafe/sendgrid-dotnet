using Newtonsoft.Json;

namespace SendGrid.Models.Mail.Tracking
{
	public class GoogleAnalyticsTracking
	{
		[JsonProperty("enable")]
		public bool Enable { get; set; }

		[JsonProperty("utm_source")]
		public string UtmSource { get; set; }

		[JsonProperty("utm_medium")]
		public string UtmMedium { get; set; }

		[JsonProperty("utm_term")]
		public string UtmTerm { get; set; }

		[JsonProperty("utm_content")]
		public string UtmContent { get; set; }

		[JsonProperty("utm_campaign")]
		public string UtmCampaign { get; set; }
	}
}
