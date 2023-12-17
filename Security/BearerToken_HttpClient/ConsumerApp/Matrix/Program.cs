using Application;
using Microsoft.Extensions.DependencyInjection;

namespace Matrix;

public class Program
{
	public static async Task Main()
	{
		var startup = new Startup();
		var host = startup.HostBuild();

		var consumer = host.Services.GetRequiredService<ProgramApplication>();

		await consumer.SignIn();
		await consumer.RunAsync();
	}
}