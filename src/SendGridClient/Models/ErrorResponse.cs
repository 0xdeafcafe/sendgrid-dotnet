using System.Collections.Generic;
using Newtonsoft.Json;
using SendGrid.Models.Error;

namespace SendGrid.Models
{
	public class ErrorResponse
	{
		[JsonProperty("errors")]
		public IEnumerable<ErrorDetail> Errors { get; set; }
	}
}
