using Newtonsoft.Json;
using SendGrid.Client.Converters;

namespace SendGrid.Models
{
	public class GenericResponse
	{
		[JsonProperty("message")]
		[JsonConverter(typeof(SuccessBoolConverter))]
		public bool Message { get; set; }
	}
}
