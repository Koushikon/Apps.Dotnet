using Domain.Interfaces;
using Domain.Models;

namespace Application;

public class ProgramApplication
{
	private readonly IUserApiRepository _userRepository;
	private readonly ILoginApiRepository _loginRepository;

	public ProgramApplication(IUserApiRepository userRepository, ILoginApiRepository loginRepository)
	{
		_userRepository = userRepository;
		_loginRepository = loginRepository;		
	}

	public async Task SignIn()
	{
		Console.WriteLine("----- Let's Sign in.");

		Console.Write("Enter the email: ");
		var email = Console.ReadLine() ?? "456@abc.com";

		Console.Write("Enter the password: ");
		var password = Console.ReadLine() ?? "1234";

		Environment.SetEnvironmentVariable("email", email);
		Environment.SetEnvironmentVariable("password", password);

		var token = await _loginRepository.AuthenticateAsync();
		Environment.SetEnvironmentVariable("token", token.Token);
	}

	public async Task RunAsync()
	{
		Console.WriteLine();

		int option;
		do
		{
			Console.WriteLine("- - - - - Menu - - - - -");
			Console.WriteLine("Choose an option:");
			Console.WriteLine(" 1) Create new User");
			Console.WriteLine(" 2) List all Users");
			Console.WriteLine(" 3) Get user by Id");
			Console.WriteLine(" 4) ReEnter credentials");
			Console.WriteLine(" 1) Quit.");
			Console.Write("  Option: ");
			option = int.Parse(Console.ReadLine() ?? "4");

			Console.WriteLine("- - -");

			switch (option)
			{
				case 1:
					var user = CreateNewUser();
					await _userRepository.CreateUserAsync(user, Environment.GetEnvironmentVariable("token"));
					break;
				case 2:
					PrintEveryUser(await GetAllUsers());
					break;
				case 3:
					var userId = GetUserId();
					PrintEveryUser(new List<UserModel> { await GetUserById(userId) });
					break;
				case 4:
					await SignIn();
					break;
				case 5:
				default:
					continue;
			}

			Console.WriteLine("- - -");
			Console.WriteLine("- - Hit any key to proceed.");
			Console.WriteLine();
		}
		while (option != 5);

		Console.WriteLine();
		Console.WriteLine("- - - End of execution. Hit <ENTER> to finish.");
		Console.ReadLine();
	}


	public async Task<UserModel> GetUserById(int userId)
	{
		return await _userRepository.GetUserAsync(userId);
	}

	public async Task<IEnumerable<UserModel>> GetAllUsers()
	{
		return await _userRepository.GetUsersAsync();
	}

	private int GetUserId()
	{
		Console.Write("Type the user id: ");
		return int.Parse(Console.ReadLine() ?? "");
	}

	private void PrintEveryUser(IEnumerable<UserModel> userModels)
	{
		var users = userModels.ToList();

		foreach (var user in users)
		{
			Console.WriteLine($"Email: {user.Email}");
		}
	}

	private UserModel CreateNewUser()
	{
		Console.Write("Type the user first name: ");
		var firstname = Console.ReadLine() ?? "";

		Console.Write("Type the user last name: ");
		var lastname = Console.ReadLine() ?? "";

		Console.Write("Type the email address: ");
		var email = Console.ReadLine() ?? "";

		Console.Write("Enter password: ");
		var password = Console.ReadLine() ?? "";

		return new UserModel
		{
			Email = email,
			FirstName = firstname,
			LastName = lastname,
			Password = password
		};
	}
}