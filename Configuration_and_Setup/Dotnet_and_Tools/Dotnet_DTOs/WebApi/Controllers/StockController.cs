using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.Comment;
using WebApi.Dtos.Stock;
using WebApi.Helpers;
using WebApi.Interfaces;
using WebApi.Mappers;

namespace WebApi.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockRepository _stockRepo;

    public StockController(IStockRepository stockRepo)
    {
        _stockRepo = stockRepo;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var stocks = await _stockRepo.GetAllAsync(query);
        var stockDto = stocks.Select(s => s.ToStockDto());
        return Ok(stockDto);
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var stockModel = await _stockRepo.GetByIdAsync(id);
        if (stockModel == null)
            return NotFound();

        return Ok(stockModel.ToStockDto());
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StockRequestFormDto srfDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var stockModel = srfDto.ToStockFromStockRequestFormDto();
        await _stockRepo.CreateAsync(stockModel);
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }


    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, StockRequestFormDto srfDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var recordInserted = await _stockRepo.UpdateAsync(id, srfDto);

        if (recordInserted == null)
            return NotFound(ModelState);

        return Ok();
    }


    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var recordDeleted = await _stockRepo.DeleteAsync(id);
        if (recordDeleted == null)
            return NotFound();

        return Ok();
    }
}