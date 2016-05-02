namespace SendGrid.Connections
{
	public class ApiKeyConnection
	{
		/// <summary>
		/// Creates a new Api Key Connection
		/// </summary>
		/// <param name="apiKey">A SendGrid Api Key</param>
		public ApiKeyConnection(string apiKey)
		{
			ApiKey = apiKey;
		}

		public string ApiKey { get; private set; }
	}
}

