using AppModels;

namespace AppDataAccess.Repository.IRepository;

public interface IMenuItemRepository : IRepository<MenuItem>
{
    void Update(MenuItem menuItem);
}