using SendGrid.Connections;
using SendGrid.Http;

namespace SendGrid.Abstracts
{
	public abstract class ApiClient
	{
		protected ApiClient(ApiKeyConnection connection)
		{
			ApiKeyConnection = connection;
			HttpConnection = new HttpConnection(ApiKeyConnection);
		}

		internal ApiKeyConnection ApiKeyConnection { get; private set; }

		internal HttpConnection HttpConnection { get; private set; }
	}
}
