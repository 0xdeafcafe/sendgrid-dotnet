using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static Newtonsoft.Json.NullValueHandling;

namespace SendGrid.Models.Mail
{
	public class Personalization
	{
		[JsonProperty("to")]
		public IEnumerable<EmailDetail> To { get; set; }

		[JsonProperty("cc")]
		public IEnumerable<EmailDetail> Cc { get; set; }

		[JsonProperty("bcc")]
		public IEnumerable<EmailDetail> Bcc { get; set; }

		[JsonProperty("subject", NullValueHandling = Ignore)]
		public string Subject { get; set; } = null;

		[JsonProperty("headers", NullValueHandling = Ignore)]
		public Dictionary<string, string> Headers { get; set; } = null;

		[JsonProperty("send_at", NullValueHandling = Ignore)]
		[JsonConverter(typeof(JavaScriptDateTimeConverter))]
		public Nullable<DateTime> SendAt { get; set; } = null;
	}
}
