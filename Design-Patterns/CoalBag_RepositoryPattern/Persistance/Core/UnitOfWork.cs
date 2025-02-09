using Infrastructure.Interfaces;
using Persistance.Core.Interface;
using Persistance.Repositories;
using Persistance.Repositories.Interfaces;

namespace Persistance.Core;

public class UnitOfWork : IUnitOfWork
{
    private readonly IRepository _context;
    public IProductRepository Product { get; private set; }

    public UnitOfWork(IRepository context)
    {
        _context = context;

        // Setting up the Services

        Product = new ProductRepository(_context);
    }

    public void Dispose() => _context.Dispose();
}