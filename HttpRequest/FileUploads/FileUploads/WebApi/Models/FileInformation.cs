namespace WebApi.Models;

public class FileInformation
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required IFormFile File { get; set; }
}