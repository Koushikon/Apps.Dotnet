
using Refit;
using RestSharp;
using System.Net.Http.Headers;

namespace Matrix;

public class Program
{
    const string API_ADDRESS = "https://localhost:7051";

    static async Task Main()
    {
        // Upload single file
        await UploadFileWithHttClientAsync();
        await UploadFileWithRestSharpAsync();
        await UploadFileWithRefitAsync();

        // Upload single file in model
        await UploadFileInModelWithHttClientAsync();
        await UploadFileInModelWithRestSharpAsync();
        await UploadFileInModelWithRefitAsync();

        // Upload multiple file
        await UploadFilesWithHttClientAsync();
    }

    #region Upload File

    private static async Task UploadFileWithHttClientAsync()
    {
        var file = "Dummy.txt";
        var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName, file);

        using var form = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

        form.Add(fileContent, "file", Path.GetFileName(filePath));

        var httpClient = new HttpClient()
        {
            BaseAddress = new Uri(API_ADDRESS)
        };

        var response = await httpClient.PostAsync($"/api/Upload/File", form);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();

        Console.WriteLine("With HttpClient Response: " + responseContent);
    }

    private static async Task UploadFileWithRestSharpAsync()
    {
        var file = "Dummy.txt";
        var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName, file);

        var client = new RestClient(API_ADDRESS);

        var request = new RestRequest("/api/Upload/File")
            .AddFile("file", filePath, "multipart/form-data");

        var response = await client.PostAsync(request);
        Console.WriteLine("With RestSharp Response: " + response.Content);
    }

    private static async Task UploadFileWithRefitAsync()
    {
        var file = "Dummy.txt";
        var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName, file);

        var api = RestService.For<IFileUploadApi>(API_ADDRESS);
        var fileInfo = new FileInfo(filePath);
        var fileInfoPart = new FileInfoPart(fileInfo, file);

        var response = await api.File(fileInfoPart);

        Console.WriteLine("With Refit Response: " + response);
    }

    #endregion


    #region Upload File In Model

    private static async Task UploadFileInModelWithHttClientAsync()
    {
        var file = "Dummy.txt";
        var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName, file);

        using var form = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

        form.Add(fileContent, "File", Path.GetFileName(filePath));
        form.Add(new StringContent("Justin.txt"), "Name");
        form.Add(new StringContent("Its an httpClient description for text file."), "Description");

        var httpClient = new HttpClient()
        {
            BaseAddress = new Uri(API_ADDRESS)
        };

        var response = await httpClient.PostAsync($"/api/Upload/FileWithModel", form);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();

        Console.WriteLine("With FileWithModel HttpClient Response: " + responseContent);
    }

    private static async Task UploadFileInModelWithRestSharpAsync()
    {
        var file = "Dummy.txt";
        var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName, file);

        var client = new RestClient(API_ADDRESS);

        var request = new RestRequest("/api/Upload/FileWithModel")
            .AddParameter("Name", "Justin.txt")
            .AddParameter("Description", "Its an restSharp description for text file.")
            .AddFile("file", filePath, "multipart/form-data");

        var response = await client.PostAsync(request);
        Console.WriteLine("With FileWithModel RestSharp Response: " + response.Content);
    }

    private static async Task UploadFileInModelWithRefitAsync()
    {
        var file = "Dummy.txt";
        var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName, file);

        var api = RestService.For<IFileUploadInModelApi>(API_ADDRESS);
        var fileInfo = new FileInfo(filePath);
        var fileInfoPart = new FileInfoPart(fileInfo, file);

        var response = await api.FileWithModel(fileInfoPart, fileInfo.Name, "Its an refit description for text file.");

        Console.WriteLine("With FileWithModel Refit Response: " + response);
    }

    #endregion


    #region Upload Files

    private static async Task UploadFilesWithHttClientAsync()
    {
        var files = new List<string> { "Sunny.txt", "Dummy.txt" };

        using var form = new MultipartFormDataContent();

        foreach (var file in files)
        {
            var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName, file);

            var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            form.Add(fileContent, "files", Path.GetFileName(filePath));
        }


        var httpClient = new HttpClient()
        {
            BaseAddress = new Uri(API_ADDRESS)
        };

        var response = await httpClient.PostAsync($"/api/Upload/Files", form);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();

        Console.WriteLine("With Multiple Files HttpClient Response: " + responseContent);
    }

    #endregion
}

// Without Model part
public interface IFileUploadApi
{
    [Multipart]
    [Post("/api/Upload/File")]
    Task<string> File([AliasAs("file")] FileInfoPart fileInfo);
}

// With Model part
public interface IFileUploadInModelApi
{
    [Multipart]
    [Post("/api/Upload/FileWithModel")]
    Task<string> FileWithModel([AliasAs("File")] FileInfoPart fileInfo, [AliasAs("Name")] string name, [AliasAs("Description")] string description);
}