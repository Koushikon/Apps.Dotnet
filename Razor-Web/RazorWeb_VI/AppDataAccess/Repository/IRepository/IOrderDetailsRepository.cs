using AppModels;

namespace AppDataAccess.Repository.IRepository;

public interface IOrderDetailsRepository : IRepository<OrderDetails>
{
    void Update(OrderDetails orderDetails);
}