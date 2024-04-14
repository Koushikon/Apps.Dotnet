using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccess.DBAccess;

public class SQLDBAccess : ISQLDBAccess
{
	private readonly IConfiguration _config;


	public SQLDBAccess(IConfiguration config)
	{
		_config = config;
	}

	public async Task<IEnumerable<T>> LoadData<T, U>(
		string storedProcedure,
		U parameters,
		string connectionId = "Default")
	{
		using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

		return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
	}

	public async Task SaveData<T>(
		string storedProcedure,
		T parameters,
		string connectionId = "Default")
	{
		using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

		await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
	}
}
