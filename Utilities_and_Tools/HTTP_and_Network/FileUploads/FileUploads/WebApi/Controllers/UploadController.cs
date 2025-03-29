using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("api/Upload")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly ILogger<UploadController> _logger;
    private IWebHostEnvironment _environment;

    public UploadController(ILogger<UploadController> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }


    [HttpPost("File")]
    public async Task<IActionResult> File(IFormFile file)
    {
        _logger.LogInformation("File: Upload: " + file.FileName);
        await FileSave(file);

        return Ok(file.FileName);
    }


    [HttpPost("Files")]
    public async Task<IActionResult> Files(IFormFileCollection files)
    {
        var names = new StringBuilder();
        foreach (var file in files)
        {
            _logger.LogInformation("Files: Upload: " + file.FileName);
            await FileSave(file);

            names.Append(file.FileName + "; ");
        }
        return Ok(names.ToString());
    }


    [HttpPost("FileWithModel")]
    public async Task<IActionResult> FileWithModel([FromForm] FileInformation fileInfo)
    {
        _logger.LogInformation("FileWithModel: Upload: " + fileInfo.File.FileName);
        await FileSave(fileInfo.File);

        return Ok(fileInfo.File.FileName);
    }


    private async Task FileSave(IFormFile file)
    {
        string uploads = Path.Combine(_environment.WebRootPath, "Uploads");
        string extension = Path.GetExtension(file.FileName);

        if (file.Length > 0)
        {
            string filePath = Path.Combine(uploads, DateTime.Now.ToString("yyMMdd-hhmmss-fffff") + extension);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }
    }
}