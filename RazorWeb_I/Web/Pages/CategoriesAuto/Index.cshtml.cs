using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Pages.CategoriesAuto;

public class IndexModel : PageModel
{
    private readonly Web.Data.ApplicationDBContext _context;

    public IndexModel(Web.Data.ApplicationDBContext context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Category != null)
        {
            Category = await _context.Category.ToListAsync();
        }
    }
}
