using UserApi.Models;

namespace UserApi.Database;

public class UsersDatabase : List<UserModel>
{
	public UsersDatabase()
	{
		Add(new UserModel
		{
			Id = this.Count + 1,
			Email = "123@abc.com",
			Password = "1234",
			FirstName = "Test",
			LastName = "User 1"
		});

		Add(new UserModel
		{
			Id = this.Count + 1,
			Email = "456@abc.com",
			Password = "1234",
			FirstName = "Test",
			LastName = "User 2"
		});


		Add(new UserModel
		{
			Id = this.Count + 1,
			Email = "789@abc.com",
			Password = "1234",
			FirstName = "Test",
			LastName = "User 3"
		});
	}

	public void AddUser(UserModel user)
	{
		user.Id = this.Count + 1;

		Add(user);
	}
}