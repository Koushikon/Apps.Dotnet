using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.FoodTypes;

[BindProperties]
public class IndexModel : PageModel
{
    private readonly IFoodTypeRepository _dbFoodType;

    public IEnumerable<FoodType> FoodTypes { get; set; } = default!;

    public IndexModel(IFoodTypeRepository dbFoodType)
    {
        _dbFoodType = dbFoodType;
    }

    public void OnGet()
    {
        FoodTypes = _dbFoodType.GetAll();
    }
}
