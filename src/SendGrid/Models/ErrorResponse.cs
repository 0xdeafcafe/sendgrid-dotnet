using Newtonsoft.Json;

namespace SendGrid.Models
{
	public class ErrorResponse
	{
		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("errors")]
		public string[] Errors { get; set; }
	}
}
