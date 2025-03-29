using Domain.DbEntities;
using Domain.Entities;
using Infrastructure.Interfaces;
using Persistance.Repositories.Interfaces;
using System.Data;

namespace Persistance.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IRepository _context;

    public ProductRepository(IRepository context)
    {
        _context = context;
    }

    public int AddEditProduct(Product product)
    {
        // avabe jodi parameter gulo set kori tahole akhane poti bar amak parameter add /delete korar janno array size change korte hobe na
        // extra kore @ add korte hoche na and r modhe kono value null hoy seta dbnull kore filter hoche
        var parameters = new Dictionary<string, object?>
        {
            { nameof(Product.Id), product.Id },
            { nameof(Product.Name), product.Name },
            { nameof(Product.Description), product.Description },
            { nameof(Product.Price), product.Price },
            { nameof(Product.Rank), product.Rank },
        };

        int result = _context.ExecuteQuerywithReturnId<int>(App_SP.AddEditProduct, parameters);
        return result;
    }

    public List<Product> GetAll(Product product)
    {
        const string sp = "ManageProduct";

        var parameters = new Dictionary<string, object?>
        {
            { nameof(Product.Id), product.Id },
            { nameof(Product.Name), product.Name },
            { nameof(Product.Description), product.Description },
            { nameof(Product.Price), product.Price },
            { nameof(Product.Rank), product.Rank },
        };

        var ds = _context.GetDataSet(App_SP.ManageProduct, parameters);

        var result = (from r in ds.Tables[0].AsEnumerable()
                      select new Product
                      {
                          Id = r.Field<int?>(nameof(Product.Id)),
                          Name = r.Field<string?>(nameof(Product.Name)),
                          Description = r.Field<string?>(nameof(Product.Description)),
                          Price = r.Field<decimal?>(nameof(Product.Price)),
                          Rank = r.Field<int?>(nameof(Product.Rank))
                      }).ToList();

        return result;
    }
}