using System.Net.Http;
using System.Threading.Tasks;

namespace SendGrid.Extentions
{
	internal static class HttpClientExtension
	{
		internal static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUrl, HttpContent content)
		{
			var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUrl) { Content = content };
			return client.SendAsync(request);
		}
	}
}
