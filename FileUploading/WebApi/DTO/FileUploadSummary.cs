namespace WebApi.DTO;

public class FileUploadSummary
{
    public int TotalFilesUpload { get; set; }
    public string TotalSizeUploaded { get; set; } = string.Empty;
    public IList<string> FilePaths { get; set; } = new List<string>();
    public IList<string> NotUploadedFiles { get; set; } = new List<string>();
}