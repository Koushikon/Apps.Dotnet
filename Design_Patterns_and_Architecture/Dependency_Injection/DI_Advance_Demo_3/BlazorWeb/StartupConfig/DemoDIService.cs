using BlazorWeb.Data;

namespace BlazorWeb.StartupConfig;

/***
 * Source: https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection
 * Register Group of Services or Class instances useful to our Application
 * in a separate place link inside a class and bring them in like MicroSoft does
 * builder.Services.AddRazorPages(); or builder.Services.AddServerSideBlazor();
 * 
 * This thing register multiple classes as services.
 * Now, We are gonna do the same.
 */

public static class DemoDIService
{
    public static IServiceCollection AddDemoInfo(this IServiceCollection services)
    {
        services.AddTransient<IDemo, Demo>();
        services.AddTransient<IDemo, UTCDemo>();    // Recommended way to use
        //services.AddTransient<IUTCDemo, UTCDemo>();
        services.AddTransient<ProcessDemo>();


        /***
         * We can also register like this
         * This is a permitted call,
         * This create a instance of the class and use that at the beggining of our Application
         * Then we can use it throught our our application
         * 
         * This way we cannot add as a Transient or Scoped service, Because using that will try to instantiate every time needs it
         */

        //services.AddTransient(new DemoWIthData(12));  // Not Work
        //services.AddScoped(new DemoWIthData(12));     // Not Work        
        services.AddSingleton(new Demo());
        //services.AddSingleton(new DemoWIthData(12));


        // But, using this way we can add Transient or Scoped services
        services.AddScoped<IDemo>(i => new Demo());
        services.AddTransient(i => new DemoWIthData(15));

        return services;
    }
}