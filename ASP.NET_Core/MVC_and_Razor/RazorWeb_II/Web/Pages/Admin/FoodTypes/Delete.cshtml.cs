using AppDataAccess.Data;
using AppModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.FoodTypes;

[BindProperties]
public class DeleteModel : PageModel
{
    private readonly ApplicationDBContext _db;

    public FoodType FoodType { get; set; } = default!;

    public DeleteModel(ApplicationDBContext db)
    {
        _db = db;
    }

    public void OnGet(int id)
    {
        if (id > 0)
        {
            FoodType = _db.FoodType.Find(id)!;   // returns the founded data
        }
    }

    public async Task<IActionResult> OnPost()
    {
        var dataCategory = _db.FoodType.Find(FoodType.Id);

        if (dataCategory != null)
        {
            _db.FoodType.Remove(dataCategory);
            await _db.SaveChangesAsync();
            TempData["success"] = $"Food Type {dataCategory.Name} deleted successfully";

            return RedirectToPage("Index");
        }

        return Page();
    }
}
