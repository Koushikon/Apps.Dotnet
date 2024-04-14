using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApi.Services.Interfaces;

namespace WebApi.Services;

public class DbService : IDbServices
{
    private readonly IDbConnection _db;
    private readonly IConfiguration _config;

    public DbService(IConfiguration config)
    {
        _config = config;
        _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
    }

    public async Task<T> Get<T>(string storedProcedure, object parameters)
    {
        IEnumerable<T> results = await _db.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        return results.FirstOrDefault();
    }

    public async Task<List<T>> GetAll<T>(string storedProcedure, object parameters)
    {
        IEnumerable<T> results = await _db.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        return results.ToList();
    }

    public T Insert<T>(string storedProcedure, object parameters)
    {
        T result;
        result = _db.Query<T>(storedProcedure, parameters, transaction: null, commandTimeout: 60, commandType: CommandType.StoredProcedure).FirstOrDefault();
        return result;
    }

    public T Update<T>(string storedProcedure, object parameters)
    {
        T result;
        result = _db.Query<T>(storedProcedure, parameters, transaction: null, commandTimeout: 60, commandType: CommandType.StoredProcedure).FirstOrDefault();
        return result;
    }
}