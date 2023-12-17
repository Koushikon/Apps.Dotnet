using Client.ApiClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/***
 * Add the Tttp Client to the application
 * To access it anywhre we just need to create a WeatherForecastClient obj
 * Then assign the obj through dependency injection
 */
builder.Services.AddHttpClient<WeatherForecastClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
