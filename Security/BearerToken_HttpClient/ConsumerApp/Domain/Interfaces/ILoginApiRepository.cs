using Domain.Models;

namespace Domain.Interfaces;

public interface ILoginApiRepository
{
	Task<AccessToken> AuthenticateAsync();
}