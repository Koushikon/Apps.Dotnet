using WebApi.Data.Models;

namespace WebApi.Data.Repositories.Abstraction;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Insert(Order order);
    void Remove(Order order);
}