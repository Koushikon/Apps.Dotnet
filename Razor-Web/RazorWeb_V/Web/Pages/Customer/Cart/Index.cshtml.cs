using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Web.Pages.Customer.Cart;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public IndexModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        CartTotal = 0;
    }

    public IEnumerable<ShoppingCart> ShoppingCartList { get; set; } = default!;
    public double CartTotal { get; set; }

    public void OnGet()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity!;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!;


        /***
         * If We use [NotMapped] attribure
         * This is not able to populate this MenuItem.Category and ManuItem.FoodType
         * To Populate child Tables in this case we need to remove [NotMapped] from those properties.
         */
        if (claim != null)
        {
            ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
                includeProperties: "MenuItem,MenuItem.Category,MenuItem.FoodType");
            
            foreach(var item in ShoppingCartList)
            {
                CartTotal += (item.MenuItem.Price * item.Count);
            }
        }
    }

    public IActionResult OnPostPlusCount(int cartId)
    {
        var cartItem = _unitOfWork.ShoppingCart.GetById(u => u.Id == cartId);

        if (cartItem != null)
        {
            _unitOfWork.ShoppingCart.IncrementCount(cartItem, 1);
            _unitOfWork.Save();
        }

        return RedirectToPage("/Customer/Cart/Index");
    }

    public IActionResult OnPostMinusCount(int cartId)
    {
        var cartItem = _unitOfWork.ShoppingCart.GetById(u => u.Id == cartId);

        const string rredirectPage = "/Customer/Cart/Index";
        if(cartItem == null)
        {
            return RedirectToPage(rredirectPage);
        }

        if(cartItem.Count != 1)
        {
            _unitOfWork.ShoppingCart.DecrementCount(cartItem, 1);
        }
        else
        {
            _unitOfWork.ShoppingCart.Remove(cartItem);
        }
        _unitOfWork.Save();

        return RedirectToPage(rredirectPage);
    }

    public IActionResult OnPostRemoveItem(int cartId)
    {
        var cartItem = _unitOfWork.ShoppingCart.GetById(u => u.Id == cartId);

        if (cartItem != null)
        {
            _unitOfWork.ShoppingCart.Remove(cartItem);
            _unitOfWork.Save();
        }

        return RedirectToPage("/Customer/Home/Index");
    }
}