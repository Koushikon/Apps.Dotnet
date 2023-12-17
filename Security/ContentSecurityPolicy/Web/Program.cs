// Source Repo: https://github.com/CodeMazeBlog/CodeMazeGuides/tree/main/aspnetcore-features/ContentSecurityPolicy

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opts =>
{
    var jsonInputFormatter = opts.InputFormatters
        .OfType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>()
        .Single();
    jsonInputFormatter.SupportedMediaTypes.Add("application/csp-report");
});
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//    endpoints.MapRazorPages();
//});
app.MapControllers();
app.MapRazorPages();

app.Run();
