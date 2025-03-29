using AppDataAccess.Data;
using AppModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Categories;

/***
 * * [BindProperties]
 * Explain: With this the whole class public properties will be available for the Page
 */

[BindProperties]
public class DeleteModel : PageModel
{
    private readonly ApplicationDBContext _db;


    /***
     * On Class Level: With [BindProperties] we don't need to explicily add each property separately
     * 
     * * [BindProperty]
     * Explain: With this we need to explicily add on each property separately and it will be available for the Page
     */

    //[BindProperty]
    public Category Category { get; set; } = default!;

    public DeleteModel(ApplicationDBContext db)
    {
        _db = db;
    }

    public void OnGet(int id)
    {
        if (id > 0)
        {
            Category = _db.Category.Find(id)!;   // returns the founded data
        }
    }


    /***
     * With [BindProperty] on Model Object we don't need to explicily add model name as parameter
     * 
     * * We Can use as Parameter: OnPost(Category category)
     */
    public async Task<IActionResult> OnPost()
    {
        var dataCategory = _db.Category.Find(Category.Id);

        if (dataCategory != null)
        {
            _db.Category.Remove(dataCategory);
            await _db.SaveChangesAsync();
            TempData["success"] = $"Category {dataCategory.Name} deleted successfully";

            return RedirectToPage("Index");
        }

        return Page();
    }
}
