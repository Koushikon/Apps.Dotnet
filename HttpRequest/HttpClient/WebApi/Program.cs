using Microsoft.Net.Http.Headers;
using Refit;
using WebApi.Client;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Best to use when dealing with lagacy applications
// Register Sample Http Client usage inside SampleClientController
builder.Services.AddHttpClient();

// Register Named Http Client usage inside NamedClientController
builder.Services.AddHttpClient("GitHub", config =>
{
    config.BaseAddress = new Uri("https://api.github.com/");

    // The GitHub API requires two headers.
    config.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/vnd.github.v3+json");
    config.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpClientFactory");
});

// Register Typed Http Client usage inside TypedClientController
builder.Services.AddHttpClient<IGitHubClient, GitHubClient>();

// Register Refit Generated Http Client usage inside GeneratedClientController
builder.Services.AddRefitClient<IRefitGitHubClient>()
    .ConfigureHttpClient(config =>
    {
        config.BaseAddress = new Uri("https://api.github.com/");

        // The GitHub API requires two headers.
        config.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/vnd.github.v3+json");
        config.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpClientFactory");
    });

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
