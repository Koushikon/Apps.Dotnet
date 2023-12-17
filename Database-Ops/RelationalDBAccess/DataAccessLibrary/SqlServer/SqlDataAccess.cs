using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace DataAccessLibrary.SqlServer
{
    public sealed class SqlDataAccess
    {
        public List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionString)
        {
            using IDbConnection connection = new SqlConnection(connectionString);
            List<T> rows = connection.Query<T>(sqlStatement, parameters).ToList();
            return rows;
        }

        public void SaveData<U>(string sqlStatement, U parameters, string connectionString)
        {
            using IDbConnection connection = new SqlConnection(connectionString);
            connection.Execute(sqlStatement, parameters);
        }

        public T SaveDataWithReurnId<T, U>(string sqlStatement, U parameters, string connectionString)
        {
            using IDbConnection connection = new SqlConnection(connectionString);
            T returnId = connection.QuerySingleOrDefault<T>(sqlStatement, parameters);
            return returnId;
        }

        // Execute Stored Procedure and get the values as OUTPUT parameters
        public dynamic ExecuteStoredProcedure(string spName, DynamicParameters parameters, string connectionString)
        {
            using IDbConnection connection = new SqlConnection(connectionString);
            connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure);

            var id = parameters.Get<int?>("ContactId");
            var message = parameters.Get<string>("ContactMsg");
            return new { id, message };
        }
    }
}
