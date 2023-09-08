using WebApi.DTO;

namespace WebApi.Services;

public interface IFileService
{
    Task<FileUploadSummary> UploadFileAsync(Stream fileStream, string contentType);
}