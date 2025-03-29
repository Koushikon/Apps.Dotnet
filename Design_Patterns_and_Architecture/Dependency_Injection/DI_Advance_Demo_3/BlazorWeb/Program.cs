using BlazorWeb.StartupConfig;

namespace BlazorWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();


        // Not registering here
        //builder.Services.AddTransient<IDemo, Demo>();
        //builder.Services.AddTransient<IDemo, UTCDemo>();
        //builder.Services.AddTransient<ProcessDemo>();


        // Now we're registering group of services in a separate class
        builder.Services.AddDemoInfo();

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
    }
}