using Infrastructure.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Core;

public class Repository : IRepository
{
	private readonly string _connectionString;
	private SqlConnection _connection;

	public Repository(string connectionString)
	{
		_connectionString = connectionString;
		_connection = null!;
	}

    public void Dispose() => GC.SuppressFinalize(this);


    public DataSet GetDataSet(string sqlQuery)
	{
		var ds = new DataSet();

		try
		{
			using var command = new SqlCommand();
			command.CommandType = CommandType.StoredProcedure;
			OpenConnection(); // Initialize Connection
			command.Connection = _connection;

			using var adapter = new SqlDataAdapter(command);
			adapter.Fill(ds);
		}
		catch (Exception ex)
		{
			throw new ApplicationException($"An error occurred while executing query procedure '{sqlQuery}'.", ex);
		}
		finally
		{
			CloseConnection();
		}

		return ds;
	}

	public DataSet GetDataSet(string storedProcedureName, IDictionary<string, object?> parameters)
	{
		var ds = new DataSet();

		try
		{
			using var command = new SqlCommand(storedProcedureName);
			command.CommandType = CommandType.StoredProcedure;
            AddCommandParameters(command, parameters);
            OpenConnection(); // Initialize Connection
			command.Connection = _connection;

			using var adapter = new SqlDataAdapter(command);
			adapter.Fill(ds);
		}
		catch (Exception ex)
		{
			throw new ApplicationException($"An error occurred while executing stored procedure '{storedProcedureName}'.", ex);
		}
		finally
		{
			CloseConnection();
		}

		return ds;
	}

	public T ExecuteQuerywithReturnId<T>(string storedProcedureName, IDictionary<string, object?> parameters)
	{
		object result;

		try
		{
			using var command = new SqlCommand(storedProcedureName);
			command.CommandType = CommandType.StoredProcedure;
            AddCommandParameters(command, parameters);
            //command.CommandText += "; SELECT SCOPE_IDENTITY();";
            OpenConnection(); // Initialize Connection
			command.Connection = _connection;

			result = command.ExecuteScalar();

			if (result == null || result == DBNull.Value)
				return default!;
		}
		catch (Exception ex)
		{
			throw new ApplicationException($"An error occurred while executing stored procedure '{storedProcedureName}'.", ex);
		}
		finally
		{
			CloseConnection();
		}

		return (T)Convert.ChangeType(result, typeof(T));
	}


	#region Methods

	private void OpenConnection()
	{
		try
		{
			_connection = new SqlConnection(_connectionString);
			_connection.Open();
		}
		catch (Exception ex)
		{
			throw new ApplicationException("An error occurred while opening the database connection.", ex);
		}
	}

	private void CloseConnection()
	{
		if (_connection != null)
		{
			_connection.Close();
			_connection.Dispose();
			_connection = null!;
		}
	}

    private static void AddCommandParameters(SqlCommand command, IDictionary<string, object?> parameters)
    {
        if (parameters == null)
            return;

        foreach (var kvp in parameters)
        {
            command.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value ?? DBNull.Value);
        }
    }


    #endregion Methods
}