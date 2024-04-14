using BusinessLogic.Interfaces;
using Dapper;
using Library.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Repositories;

public class CategoriesRepository : ICategories
{
    private readonly IConfiguration _configuration;
    private readonly SqlConnection _connection;

    public CategoriesRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }

    
    public async Task<IEnumerable<CategoriesModel>> Get()
    {
        return await _connection.QueryAsync<CategoriesModel>("GetCategories", commandType: CommandType.StoredProcedure);
    }

    public async Task<CategoriesModel> Find(Guid uId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CategoryUId", uId);
        
        return await _connection.QueryFirstOrDefaultAsync<CategoriesModel>("GetCategories", parameters, commandType: CommandType.StoredProcedure)
            ?? new CategoriesModel();
    }

    public async Task<CategoriesModel> Add(CategoriesModel model)
    {
        model.CreateDate = DateTime.Now;

        var parameters = new DynamicParameters();
        parameters.Add("@CategoryUId", Guid.NewGuid());
        parameters.Add("@Name", model.Name);
        parameters.Add("@CreateDate", model.CreateDate);
        
        await _connection.ExecuteAsync("InsertCategory", parameters, commandType: CommandType.StoredProcedure);
        return model;
    }

    public async Task<CategoriesModel> Update(CategoriesModel model)
    {
        model.UpdateDate = DateTime.Now;

        var parameters = new DynamicParameters();
        parameters.Add("@CategoryUId", model.CategoryUId);
        parameters.Add("@Name", model.Name);
        parameters.Add("@UpdateDate", model.UpdateDate);

        await _connection.ExecuteAsync("UpdateCategory", parameters, commandType: CommandType.StoredProcedure);
        return model;
    }

    public async Task<int> Remove(CategoriesModel model)
    {
        model.DeleteDate = DateTime.Now;

        var parameters = new DynamicParameters();
        parameters.Add("@CategoryUId", model.CategoryUId);
        parameters.Add("@DeleteDate", model.DeleteDate);
        
        return await _connection.ExecuteAsync("DeleteCategory", parameters, commandType: CommandType.StoredProcedure);
    }    
}