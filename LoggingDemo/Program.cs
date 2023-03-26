var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Cobfigure logging Service
builder.Host.ConfigureLogging((contaxt, logging) => {
    logging.ClearProviders();
    logging.AddConfiguration(contaxt.Configuration.GetSection("Logging"));

    // Shows log's on:
    // EventSource, EventLog, TraceSource, AzureAppServicesFile, AzureAppServicesBlob, ApplicationInsights
    logging.AddDebug();
    logging.AddConsole();
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
