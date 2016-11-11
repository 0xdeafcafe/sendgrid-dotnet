using Newtonsoft.Json;

namespace SendGrid.Models.Mail.Tracking
{
	public class OpenTracking
	{
		[JsonProperty("enable")]
		public bool Enable { get; set; }

		[JsonProperty("substitution_tag")]
		public string SubstitutionTag { get; set; }
	}
}
