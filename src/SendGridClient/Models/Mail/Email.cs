using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static Newtonsoft.Json.NullValueHandling;

namespace SendGrid.Models.Mail
{
	public class Email
	{
		[JsonProperty("personalizations")]
		public IEnumerable<Personalization> Personalizations { get; set; }

		[JsonProperty("from")]
		public EmailDetail From { get; set; }

		[JsonProperty("reply_to", NullValueHandling = Ignore)]
		public EmailDetail ReplyTo { get; set; } = null;

		[JsonProperty("subject")]
		public string Subject { get; set; }

		[JsonProperty("content")]
		public IEnumerable<Content> Content { get; set; }

		[JsonProperty("template_id", NullValueHandling = Ignore)]
		public string TemplateId { get; set; } = null;

		[JsonProperty("sections", NullValueHandling = Ignore)]
		public Dictionary<string, string> Sections { get; set; } = null;

		[JsonProperty("headers", NullValueHandling = Ignore)]
		public Dictionary<string, string> Headers { get; set; } = null;

		[JsonProperty("categories", NullValueHandling = Ignore)]
		public IEnumerable<string> Categories { get; set; } = null;

		[JsonProperty("send_at", NullValueHandling = Ignore)]
		[JsonConverter(typeof(JavaScriptDateTimeConverter))]
		public Nullable<DateTime> SendAt { get; set; } = null;

		[JsonProperty("batch_id", NullValueHandling = Ignore)]
		public string BatchId { get; set; } = null;

		[JsonProperty("ip_pool_name", NullValueHandling = Ignore)]
		public string IpPoolName { get; set; } = null;

		[JsonProperty("tracking_settings", NullValueHandling = Ignore)]
		public TrackingSettings TrackingSettings { get; set; } = null;
	}
}
