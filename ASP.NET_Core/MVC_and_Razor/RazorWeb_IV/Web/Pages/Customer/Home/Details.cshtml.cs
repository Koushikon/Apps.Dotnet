using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.Customer.Home
{
	public class DetailsModel : PageModel
	{
		private readonly IUnitOfWork _unitOfWork;
		public MenuItem MenuItem { get; set; } = default!;

		[Range(1, 50, ErrorMessage = "Please select count between 1 to 50.")]
        public int Count { get; set; }

        public DetailsModel(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void OnGet(int id)
		{
			MenuItem = _unitOfWork.MenuItem.GetById(u => u.Id == id, includeProperties: "Category,FoodType");
		}
	}
}
