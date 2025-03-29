using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Data;
using Web.Models;

namespace Web.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly ApplicationDBContext _db;
    public IEnumerable<Category> Categories { get; set; } = default!;

    public IndexModel(ApplicationDBContext db)
    {
        _db = db;
    }

    public void OnGet()
    {
        Categories = _db.Category;
    }
}
