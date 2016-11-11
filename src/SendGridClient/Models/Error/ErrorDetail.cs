using Newtonsoft.Json;

namespace SendGrid.Models.Error
{
	public class ErrorDetail
	{
		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("field")]
		public object Field { get; set; }

		[JsonProperty("help")]
		public object Help { get; set; }
	}
}
