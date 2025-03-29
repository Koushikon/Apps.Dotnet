using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Categories;

[BindProperties]
public class DeleteModel : PageModel
{
    private readonly ICategoryRepository _dbCategory;

    public Category Category { get; set; } = default!;

    public DeleteModel(ICategoryRepository dbCategory)
    {
        _dbCategory = dbCategory;
    }

    public void OnGet(int id)
    {
        if (id > 0)
        {
            Category = _dbCategory.GetById(u => u.Id == id);
        }
    }

    public async Task<IActionResult> OnPost()
    {
        var dataCategory = _dbCategory.GetById(u => u.Id == Category.Id);

        if (dataCategory != null)
        {
            _dbCategory.Remove(dataCategory);
            _dbCategory.Save();
            TempData["success"] = $"Category {dataCategory.Name} deleted successfully";

            return RedirectToPage("Index");
        }

        return Page();
    }
}
