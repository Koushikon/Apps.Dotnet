using Domain.Interfaces;
using System.Net.Http.Headers;

namespace Infrastructure.Repository.Handler;

public class LoginHalder : DelegatingHandler
{
	private readonly ILoginApiRepository _loginApiRepository;

	public LoginHalder(ILoginApiRepository loginApiRepository)
	{
		_loginApiRepository = loginApiRepository;
	}

	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		var token = await _loginApiRepository.AuthenticateAsync();

		request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

		return await base.SendAsync(request, cancellationToken);
	}
}