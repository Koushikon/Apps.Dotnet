using WebApi.Models;

namespace WebApi.Services.Interfaces;

public interface ICategoriesService
{
    Task<CategoriesModel> GetCategory(Guid id);
}