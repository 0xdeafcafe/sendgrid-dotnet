using Newtonsoft.Json;
using SendGrid.Models.Mail.Tracking;
using static Newtonsoft.Json.NullValueHandling;

namespace SendGrid.Models.Mail
{
	public class TrackingSettings
	{
		[JsonProperty("click_tracking", NullValueHandling = Ignore)]
		public ClickTracking ClickTracking { get; set; } = null;

		[JsonProperty("open_tracking", NullValueHandling = Ignore)]
		public OpenTracking OpenTracking { get; set; } = null;

		[JsonProperty("subscription_tracking", NullValueHandling = Ignore)]
		public SubscriptionTracking SubscriptionTracking { get; set; } = null;

		[JsonProperty("ganalytics", NullValueHandling = Ignore)]
		public GoogleAnalyticsTracking GoogleAnalyticsTracking { get; set; } = null;
	}
}
