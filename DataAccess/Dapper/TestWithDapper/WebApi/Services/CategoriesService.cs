using Dapper;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services;

public class CategoriesService : ICategoriesService
{
    private readonly IDbServices _dbServices;

    public CategoriesService(IDbServices dbServices)
    {
        _dbServices = dbServices;
    }

    public async Task<CategoriesModel> GetCategory(Guid id)
    {
        var parameters = new DynamicParameters();
        parameters.Add(nameof(CategoriesModel.CategoryUId), id);

        return await _dbServices.Get<CategoriesModel>("GetCategories", parameters);
    }
}