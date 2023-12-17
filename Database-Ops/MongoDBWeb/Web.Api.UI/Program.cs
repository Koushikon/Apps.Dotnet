using Web.Api.UI.Models;
using Web.Api.UI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSetting>(builder.Configuration.GetSection("ContactsDatabase"));

builder.Services.AddSingleton<ContactService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
