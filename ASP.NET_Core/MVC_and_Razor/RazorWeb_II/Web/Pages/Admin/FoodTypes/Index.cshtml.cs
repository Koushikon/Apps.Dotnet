using AppDataAccess.Data;
using AppModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.FoodTypes;

[BindProperties]
public class IndexModel : PageModel
{
    private readonly ApplicationDBContext _db;

    public IEnumerable<FoodType> FoodTypes { get; set; } = default!;

    public IndexModel(ApplicationDBContext db)
    {
        _db = db;
    }

    public void OnGet()
    {
        FoodTypes = _db.FoodType;
    }
}
