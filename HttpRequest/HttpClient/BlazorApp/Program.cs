using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpClient("GitHub", config =>
{
    config.BaseAddress = new Uri("https://api.github.com/");

    // The GitHub API requires two headers.
    config.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/vnd.github.v3+json");
    config.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpClientFactory");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
