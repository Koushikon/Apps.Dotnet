using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Pages.CategoriesAuto;

public class DeleteModel : PageModel
{
    private readonly Web.Data.ApplicationDBContext _context;

    public DeleteModel(Web.Data.ApplicationDBContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Category Category { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Category == null)
        {
            return NotFound();
        }

        var category = await _context.Category.FirstOrDefaultAsync(m => m.Id == id);

        if (category == null)
        {
            return NotFound();
        }
        else
        {
            Category = category;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null || _context.Category == null)
        {
            return NotFound();
        }
        var category = await _context.Category.FindAsync(id);

        if (category != null)
        {
            Category = category;
            _context.Category.Remove(Category);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
