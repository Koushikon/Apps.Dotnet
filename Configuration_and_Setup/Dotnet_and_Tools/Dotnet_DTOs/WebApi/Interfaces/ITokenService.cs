using WebApi.Models;

namespace WebApi.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}