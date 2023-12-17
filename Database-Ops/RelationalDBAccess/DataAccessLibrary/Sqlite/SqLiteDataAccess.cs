using Dapper;
using System.Data;
using System.Data.SQLite;

namespace DataAccessLibrary.Sqlite;

public class SqLiteDataAccess
{
    public List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionString)
    {
        using IDbConnection connection = new SQLiteConnection(connectionString);
        List<T> rows = connection.Query<T>(sqlStatement, parameters).ToList();
        return rows;
    }

    public void SaveData<U>(string sqlStatement, U parameters, string connectionString)
    {
        using IDbConnection connection = new SQLiteConnection(connectionString);
        connection.Execute(sqlStatement, parameters);
    }

    public T SaveDataWithReurnId<T, U>(string sqlStatement, U parameters, string connectionString)
    {
        using IDbConnection connection = new SQLiteConnection(connectionString);
        T returnId = connection.QuerySingleOrDefault<T>(sqlStatement, parameters);
        return returnId;
    }
}