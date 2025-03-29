using Persistance.Repositories.Interfaces;

namespace Persistance.Core.Interface;

public interface IUnitOfWork : IDisposable
{
    public IProductRepository Product { get; }
}