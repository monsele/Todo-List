using System;
using DavidProject.Data;
using DavidProject.Models;
using DavidProject.Repositories;
using DavidProject.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace David_s_Project
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
			services.AddMvc();
			var conn = "Server=DESKTOP-92LM3ID\\SQLEXPRESS;Database=DAVID-PROJ_DB;Trusted_Connection=True;MultipleActiveResultSets=true";
			services.AddDbContext<DataContext>(options => options.UseSqlServer(conn));
			services.AddScoped<IItem, itemRepository>();
			services.AddScoped<IReocurring, ReocuringRepository>();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "David Project", Version = "v1" });
			});
			services.AddIdentity<ApplicationUser, IdentityRole>(options => { }).AddEntityFrameworkStores<DataContext>();
			//services.AddAuthentication("cookies").AddCookie("cookies",options=>options.LoginPath="/Account/Login");
			services.ConfigureApplicationCookie(options =>
			{
				// Cookie settings
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

				options.LoginPath = "/Account/Login";
				options.SlidingExpiration = true;
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseAuthentication();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "David Project V1");
				c.RoutePrefix = "docs";
			});
			app.UseMvc();
		}
	}
}
