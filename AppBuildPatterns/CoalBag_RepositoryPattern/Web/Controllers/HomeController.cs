using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Persistance.Core.Interface;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        Product product = new();
        product.ProductList = _unitOfWork.Product.GetAll(product);

        return View(product);
    }

    public IActionResult UpsertProduct(int? id, Product product, string Perform)
    {
        if (Perform == "Save")
        {
            _unitOfWork.Product.AddEditProduct(product);
            return RedirectToAction(nameof(Index));
        }

        if(id != null && id > 0)
        {
            var productObj = new Product
            {
                Id = id
            };
            product.ProductList = _unitOfWork.Product.GetAll(productObj);

            if(product.ProductList.Count > 0)
            {
                product = product.ProductList[0];
            }
        }

        return View(product);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}