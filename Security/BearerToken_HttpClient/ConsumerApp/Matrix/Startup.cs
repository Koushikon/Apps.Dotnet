using Application;
using Domain.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Repository.Handler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Matrix;

public class Startup
{
	public IHost HostBuild()
	{
		var builder = new HostBuilder()
			.ConfigureServices((hostContext, services) =>
			{
				services.AddScoped<ProgramApplication>();

				services.AddHttpClient<ILoginApiRepository, LoginApiRepository>(
						c => c.BaseAddress = new Uri("https://localhost:7055"));

				services.AddScoped<LoginHalder>();

				services.AddHttpClient<IUserApiRepository, UserApiRepository>(
					c => c.BaseAddress = new Uri("https://localhost:7055"))
					.AddHttpMessageHandler<LoginHalder>();
			}).UseConsoleLifetime();

		return builder.Build();
	}
}