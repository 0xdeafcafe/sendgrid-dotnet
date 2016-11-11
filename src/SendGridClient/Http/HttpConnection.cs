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
using static Newtonsoft.Json.JsonConvert;

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

		public async Task<TResponse> GetAsync<TResponse>(string route, Dictionary<string, string> parameters = null)
			where TResponse : class, new()
		{
			return await MakeRequestAsync<TResponse, object>(HttpMethod.GET, route, parameters: parameters);
		}

		public async Task<TResponse> PostAsync<TResponse, TRequest>(string route, TRequest body, Dictionary<string, string> parameters = null)
			where TRequest : class, new()
			where TResponse : class, new()
		{
			return await MakeRequestAsync<TResponse, TRequest>(HttpMethod.POST, route, body, parameters);
		}

		public async Task<TResponse> PutAsync<TResponse, TRequest>(string route, TRequest body, Dictionary<string, string> parameters = null)
			where TRequest : class, new()
			where TResponse : class, new()
		{
			return await MakeRequestAsync<TResponse, TRequest>(HttpMethod.PUT, route, body, parameters);
		}

		public async Task<TResponse> PatchAsync<TResponse, TRequest>(string route, TRequest body, Dictionary<string, string> parameters = null)
			where TRequest : class, new()
			where TResponse : class, new()
		{
			return await MakeRequestAsync<TResponse, TRequest>(HttpMethod.PATCH, route, body, parameters);
		}

		public async Task<TResponse> DeleteAsync<TResponse, TRequest>(string route, TRequest body,  Dictionary<string, string> parameters = null)
			where TRequest : class, new()
			where TResponse : class, new()
		{
			return await MakeRequestAsync<TResponse, TRequest>(HttpMethod.DELETE, route, body, parameters);
		}

		private async Task<TResponse> MakeRequestAsync<TResponse, TRequest>(HttpMethod method, string route, TRequest body = null, Dictionary<string, string> parameters = null)
			where TRequest : class, new()
			where TResponse : class, new()
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
					new ProductInfoHeaderValue("sendgrid-dotnet", "1.0.0-beta")); // TODO: make this reaaal
				httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {_apiKey}");

				httpClient.BaseAddress = new Uri(_baseUrl);
				var path = $"/v{_version}/{route}";

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
						response = await httpClient.PostAsync(path, new StringContent(SerializeObject(body), Encoding.UTF8, _jsonContentType));
						break;

					case HttpMethod.PUT:
						response = await httpClient.PutAsync(path, new StringContent(SerializeObject(body), Encoding.UTF8, _jsonContentType));
						break;

					case HttpMethod.PATCH:
						response = await httpClient.PatchAsync(path, new StringContent(SerializeObject(body), Encoding.UTF8, _jsonContentType));
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
				{
					if (String.IsNullOrWhiteSpace(responseContent))
						return new TResponse();

					return JsonConvert.DeserializeObject<TResponse>(responseContent);
				}

				// otherwise, deserialize it into an error response and throw it
				var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
				if (errorResponse == null)
					response.EnsureSuccessStatusCode();

				throw new SendGridApiException(errorResponse);
			}
		}
	}
}
