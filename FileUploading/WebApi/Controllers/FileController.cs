using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Utilities;

namespace WebApi.Controllers;

/***
 * Learn Source: https://code-maze.com/aspnetcore-upload-large-files/amp/
 */


[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    /***
     * [RequestSizeLimit(<Size in bytes>)]
     * 
     * Also instead of globally increasing the request limit size,
     * We can use the this to specifically increase the request size limit for the specific action only.
     */
    //[RequestSizeLimit(20_971_520)]
    [HttpPost("upload-stream-multipartreader")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [MultipartFormData]
    [DisableFormValueModelBinding]
    public async Task<IActionResult> Upload()
    {
        var fileUploadSummary = await _fileService.UploadFileAsync(HttpContext.Request.Body, Request.ContentType!);

        return CreatedAtAction(nameof(Upload), fileUploadSummary);
    }
}