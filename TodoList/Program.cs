namespace TodoList
{
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Plumbing;
	using TodoList.Data;

	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(
					webBuilder =>
					{
						//webBuilder.UseStartup<Startup>(); // Works
						webBuilder.UseStartup<BrokenStartup>(); // Broken
					})
				.ConfigureServices(x => x.AddSingleton<WeatherForecastService>());
	}
}
