using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DataContext _context;        

        public IndexModel(DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }
        public List<Product> Products { get; set; }

        public void OnGet()
        {
            Products = _context.Products.ToList();
        }

        public IActionResult OnPostAddRow()
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(Product);
                _context.SaveChanges();

                return RedirectToPage();
            }

            Products = _context.Products.ToList();

            return Page();
        }

        public IActionResult OnPostRemoveRow(int id)
        {
            Product? product = _context.Products.Find(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToPage();
        }
    }
}
