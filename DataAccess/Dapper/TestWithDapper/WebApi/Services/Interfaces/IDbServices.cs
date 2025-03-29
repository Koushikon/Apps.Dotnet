namespace WebApi.Services.Interfaces;

public interface IDbServices
{
    Task<T> Get<T>(string storedProcedure, object parameters);
    Task<List<T>> GetAll<T>(string storedProcedure, object parameters);
    T Insert<T>(string storedProcedure, object parameters);
    T Update<T>(string storedProcedure, object parameters);
}