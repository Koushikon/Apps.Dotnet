using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

/***
 * Enabling Kestrel Support for the Large Files uploading
 * 
 * we are allowing the uploading of files of any size.
 * We can configure this according to our needs and requirements
 */
builder.WebHost.ConfigureKestrel(opts =>
{
    opts.Limits.MaxRequestBodySize = long.MaxValue;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFileService, FileService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }