using BusinessLogic.Interfaces;

namespace BusinessLogic.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public ICategories Categories { get; set; }

    public UnitOfWork(ICategories categories)
    {
        Categories = categories;
    }
}