using Domain.Models;

namespace Domain.Interfaces;

public interface IUserApiRepository
{
	Task CreateUserAsync(UserModel userModel, string token);
	Task<UserModel> GetUserAsync(int userId);
	Task<IEnumerable<UserModel>> GetUsersAsync();
}