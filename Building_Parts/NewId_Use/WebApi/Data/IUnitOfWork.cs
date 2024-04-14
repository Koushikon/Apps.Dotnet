using WebApi.Data.Repositories;

namespace WebApi.Data;

public interface IUnitOfWork
{
    OrderRepository Order { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}