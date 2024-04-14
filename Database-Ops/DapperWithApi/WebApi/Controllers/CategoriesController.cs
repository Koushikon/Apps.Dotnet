using BusinessLogic.Interfaces;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/Categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IEnumerable<CategoriesModel>> GetCategories()
    {
        return await _unitOfWork.Categories.Get();
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetCategory(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest();

        CategoriesModel category = await _unitOfWork.Categories.Find(id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(CategoriesModel model)
    {
        if (model == null)
        {
            return BadRequest(ModelState);
        }

        await _unitOfWork.Categories.Add(model);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory(CategoriesModel model)
    {
        if (model == null || model.CategoryUId == Guid.Empty)
        {
            return BadRequest(ModelState);
        }

        CategoriesModel category = await _unitOfWork.Categories.Find(model.CategoryUId);

        if (category == null)
        {
            return NotFound();
        }

        category.Name = model.Name;
        await _unitOfWork.Categories.Update(category);
        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> RemoveCategory(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(ModelState);
        }

        CategoriesModel category = await _unitOfWork.Categories.Find(id);

        if (category == null)
        {
            return NotFound();
        }

        await _unitOfWork.Categories.Remove(category);
        return Ok();
    }
}