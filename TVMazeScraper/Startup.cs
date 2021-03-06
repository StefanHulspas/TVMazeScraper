using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVMazeScraper.Data;
using TVMazeScraper.Options;
using TVMazeScraper.Services;

namespace TVMazeScraper
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			var dataContext = new DataContext();
			services.AddSingleton(dataContext);
			services.AddSingleton<IShowService, ShowService>();
			services.AddSwaggerGen(x => {
				x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TVMazeScraper API", Version = "v1"});
			});
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
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			var swaggerOptions = new SwaggerOptions();
			Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

			app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; } );
			app.UseSwaggerUI(option => { option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description); });


			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
