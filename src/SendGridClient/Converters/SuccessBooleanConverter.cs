using System;
using Newtonsoft.Json;

namespace SendGrid.Client.Converters
{
	public class SuccessBoolConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return (reader.Value.ToString().ToLowerInvariant() == "success");
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(bool);
		}
	}
}
