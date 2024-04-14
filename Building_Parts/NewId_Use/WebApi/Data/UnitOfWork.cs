using WebApi.Data.Repositories;

namespace WebApi.Data;

public class UnitOfWork(OrdersDbContext context) : IUnitOfWork
{
    public OrderRepository Order { get; } = new OrderRepository(context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        await context.SaveChangesAsync(cancellationToken);
}