using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWeb.Data;
using System.ComponentModel.DataAnnotations;

namespace RazorWeb.Pages;

public class BufferedDoubleFileUploadDbModel : PageModel
{
    private readonly FilesUploadDataContext _context;
    private readonly long _fileSizeLimit;
    private readonly string[] _permittedExtensions = [".txt", ".jpg"];

    public BufferedDoubleFileUploadDbModel(FilesUploadDataContext context, IConfiguration config)
    {
        _context = context;
        _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
    }

    [BindProperty]
    public BufferedDoubleFileUploadDb FilesUpload { get; set; }
    public string Result { get; private set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostUploadAsync()
    {

    }
}

public class BufferedDoubleFileUploadDb
{
    [Required]
    [Display(Name = "File 1")]
    public IFormFile FormFile1 { get; set; }

    [Required]
    [Display(Name = "File 1")]
    public IFormFile FormFile2 { get; set; }

    [Display(Name = "Note")]
    [StringLength(50, MinimumLength = 0)]
    public string? Note { get; set; }
}