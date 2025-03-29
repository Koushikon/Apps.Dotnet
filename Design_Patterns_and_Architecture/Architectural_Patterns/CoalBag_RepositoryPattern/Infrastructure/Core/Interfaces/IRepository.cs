using System.Data;

namespace Infrastructure.Interfaces;

public interface IRepository : IDisposable
{
    DataSet GetDataSet(string sqlQuery);

    DataSet GetDataSet(string storedProcedureName, IDictionary<string, object?> parameters);

    T ExecuteQuerywithReturnId<T>(string storedProcedureName, IDictionary<string, object?> parameters);
}