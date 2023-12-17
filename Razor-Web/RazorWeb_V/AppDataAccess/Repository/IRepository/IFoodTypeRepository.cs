using AppModels;

namespace AppDataAccess.Repository.IRepository;

public interface IFoodTypeRepository : IRepository<FoodType>
{
    void Update(FoodType foodType);
}