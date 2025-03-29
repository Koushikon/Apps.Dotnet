using Microsoft.EntityFrameworkCore;
using WebApi.Data.Models;
using WebApi.Data.Repositories.Abstraction;

namespace WebApi.Data.Repositories;

public class OrderRepository(OrdersDbContext context) : IOrderRepository
{
    public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await context.Orders.ToListAsync(cancellationToken);

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context.Orders.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public void Insert(Order order) =>
        context.Orders.Add(order);

    public void Remove(Order order) =>
        context.Orders.Remove(order);
}