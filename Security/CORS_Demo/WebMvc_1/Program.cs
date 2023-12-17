var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddCors(opts =>
{
    // We can define one or more named policies with their rules
    opts.AddPolicy("Policy1", policy =>
    {
        policy.WithOrigins("http://localhost:5086")
            .AllowAnyHeader()
            .WithMethods("GET", "POST", "PUT", "DELETE");
    });

    // We can define a default policy that applies to every request. 
    opts.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:50860");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Defina the named policy,
// Then select which policy to apply using the policy name at middleware. 
app.UseCors("Policy1");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
