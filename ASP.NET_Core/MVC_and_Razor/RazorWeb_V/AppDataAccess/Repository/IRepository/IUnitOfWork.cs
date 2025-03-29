namespace AppDataAccess.Repository.IRepository;

/***
 * Its just a Wrapper for all the Repositories in your Project
 */

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository Category { get; }

    IFoodTypeRepository FoodType { get; }

    IMenuItemRepository MenuItem { get; }

    IShoppingCartRepository ShoppingCart { get; }

    void Save();
}