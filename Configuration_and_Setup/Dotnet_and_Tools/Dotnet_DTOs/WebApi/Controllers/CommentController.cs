using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.Comment;
using WebApi.Interfaces;
using WebApi.Mappers;

namespace WebApi.Controllers;

[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepo;
    private readonly IStockRepository _stockRepo;

    public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
    {
        _commentRepo = commentRepo;
        _stockRepo = stockRepo;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var comments = await _commentRepo.GetAllAsync();
        var commentDto = comments.Select(s => s.ToCommentDto());
        return Ok(commentDto);
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var commentModel = await _commentRepo.GetByIdAsync(id);
        if (commentModel == null)
            return NotFound();

        return Ok(commentModel.ToCommentDto());
    }


    [HttpPost]
    [Route("{stockId:int}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, CommentFormDto commentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await _stockRepo.StockExists(stockId))
            return BadRequest("Stock does not exist.");

        var commentModel = commentDto.ToCommentFromCommentFormDto(stockId);
        await _commentRepo.CreateAsync(commentModel);
        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
    }

    [HttpPut]
    [Route("{stockId:int}")]
    public async Task<IActionResult> Update([FromRoute] int stockId, CommentFormDto commentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var recordInserted = await _commentRepo.UpdateAsync(stockId, commentDto);

        if(recordInserted == null)
            return NotFound(ModelState);

        return Ok();
    }


    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var recordDeleted = await _commentRepo.DeleteAsync(id);
        if (recordDeleted == null)
            return NotFound();

        return Ok();
    }
}