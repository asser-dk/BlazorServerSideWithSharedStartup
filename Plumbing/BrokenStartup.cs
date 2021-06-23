namespace Plumbing
{
	using System.Reflection;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.FileProviders;
	using Microsoft.Extensions.Hosting;

	public class BrokenStartup
	{
		public BrokenStartup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			var assembly = Assembly.GetEntryAssembly();

			services
				.AddRazorPages()

				// Remove these two parts and you get an error saying `Cannot find the fallback endpoint specified by route values: { page: /_Host, area:  }` when running the app
				.AddRazorRuntimeCompilation(
					options =>
					{
						options.FileProviders.Add(new EmbeddedFileProvider(assembly)); // Adding this changed the fallback endpoint error to a "Predefined type System.Xyz not found"
					})
				.AddApplicationPart(assembly); // Removes the compilation errors caused by adding the file provider above.

			services.AddServerSideBlazor();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");

				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(
				endpoints =>
				{
					endpoints.MapBlazorHub();
					endpoints.MapFallbackToPage("/_Host");
				});
		}
	}
}
