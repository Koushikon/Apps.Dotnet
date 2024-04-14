using System.ComponentModel.DataAnnotations;

namespace RazorWeb.Models;

public class AppFile
{
    public int Id { get; set; }

    public byte[] Content { get; set; }

    [Display(Name = "Generated File Name")]
    public string TrustedName { get; set; }

    [Display(Name = "Real File Name")]
    public string UntrustedName { get; set; }

    [Display(Name = "Note")]
    public string? Note { get; set; }

    [Display(Name = "Size (bytes)")]
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public long Size { get; set; }

    [Display(Name = "Uploaded (UTC)")]
    public DateTime UploadDT { get; set; }
}