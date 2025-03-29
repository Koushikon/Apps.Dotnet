using WebApi.Extensions;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<FactoryActivatedCustomMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();


/***
 * Using our Custom Middleware
 */
app.UseCustomMiddleware();
app.UseFactoryActivatedCustomMiddleware();


/***
 * Map extensions are used as a convention for branching the pipeline.
 * When we provide the pathMatch string, the Map method will compare it
 * to the start of the request path. If they match, the app will execute the branch.
 */
app.Map("/map1", HandleMapTest1);

app.Map("/map2", builder =>
{
    builder.Use(async (context, next) =>
    {
        Console.WriteLine("Map branch logic in the Use method before the next delegate");
        await next.Invoke();
        Console.WriteLine("Map branch logic in the Use method after the next delegate");
    });

    builder.Run(async context =>
    {
        Console.WriteLine($"Map branch response to the client in the Run method");
        await context.Response.WriteAsync("Hello from the map 2 branch.");
    });
});

static void HandleMapTest1(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("Map Test 1");
    });
}


// Map supports nesting, for example:
app.Map("/level1", level1App => {
    level1App.Map("/level1a", level1AApp => {
        // "/level1/level1a" processing

        level1AApp.Run(async context =>
        {
            await context.Response.WriteAsync("Hello from the map 2 A branch.");
        });
    });

    level1App.Map("/level1b", level2BApp => {
        // "/level1/level1b" processing

        level2BApp.Run(async context =>
        {
            await context.Response.WriteAsync("Hello from the map 2 B branch.");
        });
    });
});


// Map can also match multiple segments at once:
app.Map("/level2/level2a", HandleLavelBMap);

static void HandleLavelBMap(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("Coming from Level 2-A");
    });
}


/***
 * If our request contains the provided query string,
 * we execute the Run method by writing the response to the client.
 * Any predicate of type Func<HttpContext, bool> can be used
 * to map requests to a new branch of the pipeline.
 */
app.MapWhen(context => context.Request.Query.ContainsKey("employee"), builder =>
{
    builder.Run(async context =>
    {
        await context.Response.WriteAsync($"Hello from query string employee: {context.Request.Query["employee"]}");
    });
});


/***
 * UseWhen also branches the request pipeline based on the result of the given predicate.
 * Unlike with MapWhen, this branch is rejoined to the main pipeline if it doesn't
 * short-circuit or contain a terminal middleware:
 * 
 * In this case a response of Hello from non-Map delegate. is written for not some requests.
 * And If the request includes a query string variable branch, its value is logged before
 * the main pipeline is rejoined.
 */
app.UseWhen(context => context.Request.Query.ContainsKey("department"), HandleBranchAndRejoin);

void HandleBranchAndRejoin(IApplicationBuilder app)
{
    var logger = app.ApplicationServices.GetRequiredService<ILogger<Program>>();

    app.Use(async (context, next) =>
    {
        var department = context.Request.Query["department"];
        logger.LogInformation("Coming from department: {department}", department);

        // Do work that doesn't write to the Response.
        await next();
        // Do other work that doesn't write to the Response.
    });
}


app.Run(async context =>
{
    await context.Response.WriteAsync("Hello from non-Map delegate.");
});


/***
 * Chain multiple request delegates together with Use.
 * The next parameter represents the next delegate in the pipeline.
 * You can short-circuit the pipeline by not calling the next parameter.
 * You can typically perform actions both before and after the next delegate,
 * as the following example:
 * 
 * When a delegate doesn't pass a request to the next delegate,
 * it's called short-circuiting the request pipeline.
 * 
 * Don't call next.Invoke() after the response has been sent to the client.
 * Changes to HttpResponse after the response has started throw an exception.
 * For example, setting headers and a status code throw an exception.
 */
app.Use(async (context, next) =>
{
    // Do work that can write to the response.
    Console.WriteLine("Logic before executing the next delegate in the Use method.");

    //await context.Response.WriteAsync("Hello from middleware component 1."); // Causes an Error

    await next.Invoke();

    // Do logging or other work that doesn't write to the response.
    Console.WriteLine("Logic after executing the next delegate in the Use method.");
});


// Can work multiple Use delegate in the pipeline
app.Use(async (context, next) =>
{
    // Do work that can write to the response.
    Console.WriteLine("Logic before executing the next delegate in the Use method.");

    await next.Invoke();

    // Do logging or other work that doesn't write to the response.
    Console.WriteLine("Logic after executing the next delegate in the Use method.");
});


/***
 * The Run delegate is always terminal and terminates the pipeline.
 * In this case Run delegate Writes "Hello from middleware component 1."
 * to the response and terminates the pipeline. If another Use or Run delegate
 * is added after this, its not called.
 * 
 * This method accepts a single parameter of the RequestDelegate type.
 * Inspecting that we can see that it accepts a single HttpContext parameter.
 */

app.Run(async context =>
{
    /***
     * Here we write a response to the client and then call next.Invoke.
     * Of course, this passes the execution to the next component in the pipeline.
     * There, we try to set the status code of the response and write another one. 
     */
    //context.Response.StatusCode = 200; // Cause an Error

    await context.Response.WriteAsync("Hello from middleware component 1.");    // Using Microsoft.AspNetCore.Http namespace
    Console.WriteLine("Writing the response to the client in the Run method 1st.");
});


app.Run(async context =>
{
    // Its not gonna called

    await context.Response.WriteAsync("Hello from middleware component 2.");
    Console.WriteLine("Writing the response to the client in the Run method 2nd.");
});

app.MapControllers();

app.Run();