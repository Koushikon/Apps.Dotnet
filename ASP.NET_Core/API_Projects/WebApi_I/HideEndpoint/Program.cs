using HideEndpoint.Conventions;
using HideEndpoint.Filters;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers();

// Using the IActionModelConvention Convention to Hide a Api endpoint from swagger
builder.Services.AddControllers(opts =>
{
	opts.Conventions.Add(new HideControllerConvention());
});

builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddSwaggerGen();

// Using DocInclusionPredicate to Hide a Api endpoint from swagger
builder.Services.AddSwaggerGen(opts =>
{
	// Using IDocumentFilter to Hide a Api endpoint from swagger
	opts.DocumentFilter<SwaggerDocumentFilter>();

	opts.DocInclusionPredicate((docName, docDesc) =>
	{
		var routeTemplate = docDesc.RelativePath;

		if (routeTemplate == "WeatherForecast/GetWeatherForecast")
		{
			return false;
		}

		return true;
	});
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
