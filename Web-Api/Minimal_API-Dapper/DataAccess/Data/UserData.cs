using DataAccess.DBAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class UserData : IUserData
{
	private readonly ISQLDBAccess _db;
	private readonly Dictionary<int, string> _spName = new Dictionary<int, string>()
	{
		[1] = "dbo.AddPerson",
		[2] = "dbo.EditPerson",
		[3] = "dbo.GetPerson",
		[4] = "dbo.GetAllPerson",
		[5] = "dbo.DeletePerson"
	};

	public UserData(ISQLDBAccess db)
	{
		_db = db;
	}

	#region Accessing Methods

	public Task InsertUser(UserModel user) =>
		_db.SaveData(storedProcedure: _spName[1],
			new { user.FirstName, user.LastName, user.StreetAddress, user.Ciy, user.State, user.ZipCode });

	public Task UpdateUser(UserModel user) =>
		_db.SaveData(
			storedProcedure: _spName[2],
			new { user.PersonId, user.FirstName, user.LastName, user.StreetAddress, user.Ciy, user.State, user.ZipCode });

	public async Task<UserModel?> GetUser(int id)
	{
		var results = await _db.LoadData<UserModel, dynamic>(_spName[3], new { Id = id });
		return results.FirstOrDefault();
	}

	public Task<IEnumerable<UserModel>> GetUsers() =>
			_db.LoadData<UserModel, dynamic>(storedProcedure: _spName[4], new { });

	public Task DeleteUser(int id) =>
		_db.SaveData(_spName[5], new { Id = id });

	#endregion Accessing Methods
}
