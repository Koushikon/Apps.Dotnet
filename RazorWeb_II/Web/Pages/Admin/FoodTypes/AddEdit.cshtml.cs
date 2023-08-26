using AppDataAccess.Data;
using AppModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.FoodTypes;

[BindProperties]
public class AddEditModel : PageModel
{
    private readonly ApplicationDBContext _db;

    public FoodType FoodType { get; set; } = default!;

    public AddEditModel(ApplicationDBContext db)
    {
        _db = db;
    }

    public void OnGet(int id)
    {
        if (id > 0)
        {
            FoodType = _db.FoodType.Find(id)!;
        }
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            string? message = null;
            if (FoodType.Id <= 0)
            {
                await _db.FoodType.AddAsync(FoodType);
                message = $"Food Type {FoodType.Name} created successfully";
            }
            else
            {
                _db.FoodType.Update(FoodType);
                message = $"Food Type {FoodType.Name} updated successfully";
            }
            await _db.SaveChangesAsync();
            TempData["success"] = message;

            return RedirectToPage("Index");
        }

        return Page();
    }
}
