using Domain.Entities;

namespace Persistance.Repositories.Interfaces;

public interface IProductRepository
{
    int AddEditProduct(Product product);

    List<Product> GetAll(Product product);
}