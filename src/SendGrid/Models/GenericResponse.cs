using Newtonsoft.Json;

namespace SendGrid.Models
{
	public class GenericResponse
	{
		[JsonProperty("message")]
		public bool Message { get; set; }
	}
}
