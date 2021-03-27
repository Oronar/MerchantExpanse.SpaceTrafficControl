using MerchantExpanse.SpaceTraders.Contracts;
using MerchantExpanse.SpaceTraders.Factories;
using MerchantExpanse.SpaceTrafficControl.Api.Configurations;
using MerchantExpanse.SpaceTrafficControl.Api.Services;
using MerchantExpanse.SpaceTrafficControl.Api.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MerchantExpanse.SpaceTrafficControl.Api
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
			services.AddControllers();
			services.AddTransient<ISpaceTrafficService, SpaceTrafficService>();

			var merchantExpanseConfig = Configuration.GetSection("MerchantExpanse").Get<MerchantExpanseConfig>();

			services.AddSingleton<IClient>(ClientFactory.Initialize(merchantExpanseConfig.ApiToken, merchantExpanseConfig.Username));

			services.AddCors(options =>
			{
				options.AddDefaultPolicy(builder =>
				{
					builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader();
				});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseCors();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}