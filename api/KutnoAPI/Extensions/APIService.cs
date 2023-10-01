using System.Net.Http.Headers;
using System.Text.Json;

namespace KutnoAPI.Extensions;

public class APIService
{
	public static async Task<string> CallApi(HttpClient httpClient, string url, HttpMethod httpMethod, object? body = null, Dictionary<string, string>? headers = null)
	{
		HttpRequestMessage httpRequest = new(httpMethod, url);
		if (body is not null)
		{
			string json = JsonSerializer.Serialize(body);
			StringContent content = new(json);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			httpRequest.Content = content;
		}

		if (headers is not null)
		{
			foreach (var header in headers)
			{
				httpRequest.Headers.Add(header.Key, header.Value);
			}
		}

		var response = await httpClient.SendAsync(httpRequest);

		var result = await response.Content.ReadAsStringAsync();

		if (!response.IsSuccessStatusCode)
		{
			throw new Exception(result);
		}
		return result;

	}
}
