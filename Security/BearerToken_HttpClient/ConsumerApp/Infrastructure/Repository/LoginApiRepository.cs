using Domain.Interfaces;
using Domain.Models;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Repository;

public class LoginApiRepository : ILoginApiRepository
{
	private readonly HttpClient _httpClient;

	public LoginApiRepository(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<AccessToken> AuthenticateAsync()
	{
		var token = RetriveCachedToken();
		if (!string.IsNullOrWhiteSpace(token))
		{
			return new() { Token = token };
		}

		var result = await _httpClient.PostAsync("api/auth/login", GenerateBody());

		result.EnsureSuccessStatusCode();

		var response = await result.Content.ReadAsStringAsync();
		var deserializedToken = DeserializeResponse<AccessToken>(response);

		if (deserializedToken == null)
		{
			return new() { Token = "No Token" };
		}

		SetCacheToken(deserializedToken);
		return deserializedToken;
	}

	private StringContent? GenerateBody()
	{
		var email = Environment.GetEnvironmentVariable("email");
		var password = Environment.GetEnvironmentVariable("password");

		var body = JsonSerializer.Serialize(
			new { email, password },
			new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
		);

		return new StringContent(body, Encoding.UTF8, "application/json");
	}

	private void SetCacheToken(AccessToken accessToken)
	{
		// In a real world situation we should store the token in a cache service and set an TTL.
		Environment.SetEnvironmentVariable("token", accessToken.Token);
	}

	private string? RetriveCachedToken()
	{
		// In a real world situation we should retrive the token from a cache service.
		return Environment.GetEnvironmentVariable("token");
	}

	private T? DeserializeResponse<T>(string response)
	{
		return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
	}
}