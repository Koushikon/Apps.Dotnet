using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[Route("api/Categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoryService;

    public CategoriesController(ICategoriesService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetCategory(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest();

        CategoriesModel category = await _categoryService.GetCategory(id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }
}