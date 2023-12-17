using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Categories;

public class IndexModel : PageModel
{
    private readonly ICategoryRepository _dbCategory;
    public IEnumerable<Category> Categories { get; set; } = default!;

    public IndexModel(ICategoryRepository dbCategory)
    {
        _dbCategory = dbCategory;
    }

    public void OnGet()
    {
        Categories = _dbCategory.GetAll();
    }
}
