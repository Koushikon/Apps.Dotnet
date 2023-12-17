using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.MenuItems;

public class IndexModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    public IEnumerable<MenuItem> MenuItems { get; set; } = default!;

    public IndexModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void OnGet()
    {
        //MenuItems = _unitOfWork.MenuItem.GetAll();
    }
}