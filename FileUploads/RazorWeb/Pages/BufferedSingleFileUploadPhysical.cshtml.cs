using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWeb.Utilities;
using System.ComponentModel.DataAnnotations;

namespace RazorWeb.Pages;

public class BufferedSingleFileUploadPhysicalModel : PageModel
{
    private readonly long _fileSizeLimit;
    private readonly string[] _permittedExtensions = [".txt", ".jpg"];
    private readonly string _targetFilePath;

    public BufferedSingleFileUploadPhysicalModel(IConfiguration config)
    {
        _fileSizeLimit = config.GetValue<long>("FileSizeLimit");

        // To save physical files to a path provided by configuration:
        _targetFilePath = config.GetValue<string>("StoredFilesPath") ?? string.Empty;

        // To save physical files to the temporary files folder, use:
        //_targetFilePath = Path.GetTempPath();
    }

    [BindProperty]
    public BufferedSingleFileUploadPhysical FileUpload { get; set; }
    public string Result { get; private set; }

    public void OnGet()
    { }


    /***
     * If we dont provide the Handler name after OnPost and still provide the handler name in cshtml
     * the default OnPost method will hit, Otherwise the Matched Handler method
     * 
     * We do not need to add the Async suffix. The option is provided for developers who prefer to use
     * the Async suffix on methods that contain asynchronous code.
     */
    public async Task<IActionResult> OnPostUploadAsync()
    {
        // Perform an initial check to catch FileUpload class
        // attribute violations.
        if (!ModelState.IsValid)
        {
            Result = "Please correct the form.";
            return Page();
        }

        var formFileContent = await FileHelpers.ProcessFormFile<BufferedSingleFileUploadPhysical>(
            FileUpload.FormFile, ModelState, _permittedExtensions, _fileSizeLimit);

        // Perform a second check to catch ProcessFormFile method
        // violations. If any validation check fails, return to the
        // page.
        if (!ModelState.IsValid)
        {
            Result = "Please correct the form.";
            return Page();
        }

        // For the file name of the uploaded file stored
        // server-side, use Path.GetRandomFileName to generate a safe
        // random file name.
        var trustedFileNameForFileStorage = Path.GetRandomFileName();
        var filePath = Path.Combine(_targetFilePath, trustedFileNameForFileStorage);

        // **WARNING!**
        // In the following example, the file is saved without
        // scanning the file's contents. In most production
        // scenarios, an anti-virus/anti-malware scanner API
        // is used on the file before making the file available
        // for download or for use by other systems. 
        // For more information, see the topic that accompanies 
        // this sample.

        using (var fileStream = System.IO.File.Create(filePath))
        {
            await fileStream.WriteAsync(formFileContent);

            // To work directly with a FormFile, use the following
            // instead:
            //await FileUpload.FormFile.CopyToAsync(fileStream);
        }

        return RedirectToPage("./Index");
    }
}

public class BufferedSingleFileUploadPhysical
{
    [Required]
    [Display(Name = "File")]
    public IFormFile FormFile { get; set; }

    [Display(Name = "Note")]
    [StringLength(50, MinimumLength = 0)]
    public string? Note { get; set; }
}