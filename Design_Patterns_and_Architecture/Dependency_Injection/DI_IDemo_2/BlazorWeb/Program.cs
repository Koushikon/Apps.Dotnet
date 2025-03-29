using BlazorWeb.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            // Use Carefully
            builder.Services.AddTransient<IDemo, Demo>();
            builder.Services.AddTransient<IDemo, UTCDemo>();
            //builder.Services.AddTransient<IUTCDemo, UTCDemo>();
            builder.Services.AddTransient<ProcessDemo>();

            //builder.Services.AddScoped<IDemo, Demo>();
            //builder.Services.AddScoped<IUTCDemo, UTCDemo>();
            //builder.Services.AddScoped<ProcessDemo>();

            //builder.Services.AddSingleton<IDemo, Demo>();
            //builder.Services.AddSingleton<IUTCDemo, UTCDemo>();
            //builder.Services.AddSingleton<ProcessDemo>();

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

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}