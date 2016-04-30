using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SendGrid.Connections;
using SendGrid.Exceptions;
using SendGrid.Extentions;
using SendGrid.Models;

namespace SendGrid.Http
{
	public class HttpConnection
	{
		private const string _baseUrl = "https://api.sendgrid.com/";
		private const ushort _version = 3;
		private const string _jsonContentType = "application/json";

		private TimeSpan _timeout = TimeSpan.FromMilliseconds(10000);
		private string _apiKey = null;

		private enum HttpMethod
		{
			GET,
			POST,
			PUT,
			PATCH,
			DELETE
		}

		internal HttpConnection(ApiKeyConnection connection)
		{
			_apiKey = connection.ApiKey;
		}

		public async Task<T> GetAsync<T>(string route, Dictionary<string, string> parameters = null)
		{
			return await MakeRequestAsync<T>(HttpMethod.GET, route, parameters: parameters);
		}

		public async Task<T> PostAsync<T>(string route, Dictionary<string, string> body, Dictionary<string, string> parameters = null)
		{
			return await MakeRequestAsync<T>(HttpMethod.POST, route, body, parameters);
		}

		public async Task<T> PutAsync<T>(string route, Dictionary<string, string> body, Dictionary<string, string> parameters = null)
		{
			return await MakeRequestAsync<T>(HttpMethod.PUT, route, body, parameters);
		}

		public async Task<T> PatchAsync<T>(string route, Dictionary<string, string> body, Dictionary<string, string> parameters = null)
		{
			return await MakeRequestAsync<T>(HttpMethod.PATCH, route, body, parameters);
		}

		public async Task<T> DeleteAsync<T>(string route, Dictionary<string, string> parameters = null)
		{
			return await MakeRequestAsync<T>(HttpMethod.DELETE, route, parameters);
		}

		private async Task<T> MakeRequestAsync<T>(HttpMethod method, string route, Dictionary<string, string> body = null, Dictionary<string, string> parameters = null)
		{
			SortedDictionary<string, string> sortedParameters = parameters == null
				? sortedParameters = new SortedDictionary<string, string>()
				: sortedParameters = new SortedDictionary<string, string>(parameters);

			// Uncomment the HttpClientHandler to run requests through Fiddler to aid debugging
			using (var httpClient = new HttpClient(/*new HttpClientHandler { Proxy = new WebProxy("localhost:8888", true), UseDefaultCredentials = true }*/))
			{
				httpClient.Timeout = _timeout;

				// Set required headers
				httpClient.DefaultRequestHeaders.Accept.Add(
					new MediaTypeWithQualityHeaderValue(_jsonContentType));
				httpClient.DefaultRequestHeaders.UserAgent.Add(
					new ProductInfoHeaderValue("sendgrid_dotnet", "1.0.0-beta")); // TODO: make this reaaal
				httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {_apiKey}");

				httpClient.BaseAddress = new Uri(_baseUrl);
				var path = $"/api/{route}.json";

				// Add additional required parameters
				sortedParameters.Add("timestamp", $"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff")}Z");
				
				path = $"{path}?{sortedParameters.ToQueryString()}";

				HttpResponseMessage response = null;
				switch (method)
				{
					case HttpMethod.GET:
						response = await httpClient.GetAsync(path);
						break;

					case HttpMethod.POST:
						response = await httpClient.PostAsync(path, new FormUrlEncodedContent(body));
						break;

					case HttpMethod.PUT:
						response = await httpClient.PutAsync(path, new FormUrlEncodedContent(body));
						break;

					case HttpMethod.PATCH:
						response = await httpClient.PatchAsync(path, new FormUrlEncodedContent(body));
						break;

					case HttpMethod.DELETE:
						response = await httpClient.DeleteAsync(path);
						break;

					default:
						throw new NotImplementedException($"The http method '{method.ToString()}' is not supported.");
				}

				// read the response content into a string
				var responseContent = await response.Content.ReadAsStringAsync();

				// if the response was a success: deserialize the response and then return it.
				if (response.IsSuccessStatusCode)
					return JsonConvert.DeserializeObject<T>(responseContent);

				// otherwise, deserialize it into an error response and throw it
				var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
				if (errorResponse == null)
					response.EnsureSuccessStatusCode();

				throw new SendGridApiException(errorResponse);
			}
		}
	}
}
