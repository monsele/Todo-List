using System;
using DavidProject.Data;
using DavidProject.Models;
using DavidProject.Repositories;
using DavidProject.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Hangfire;
using Hangfire.SqlServer;
using DavidProject.Controllers;
using Microsoft.AspNetCore.Http;

namespace David_s_Project
{
    public class Startup
    {
        private readonly IMonthlies task;
        private readonly UserManager<ApplicationUser> userManager;
       
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        //MonthliesController obj = new MonthliesController(userManager, task);
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(options =>
                 {
                     options.LoginPath = "/Account/Login/";
                 });
            services.AddIdentity<ApplicationUser, IdentityRole>(options => { }).AddEntityFrameworkStores<DataContext>();
            services.AddMvc().AddControllersAsServices();
            var conn = "Server=DESKTOP-92LM3ID\\SQLEXPRESS;Database=David_DB;Trusted_Connection=True;MultipleActiveResultSets=true";
            var hangfireConn= "Server=DESKTOP-92LM3ID\\SQLEXPRESS;Database=HangFireDB;Trusted_Connection=True;MultipleActiveResultSets=true";
            services.AddDbContext<DataContext>(options => options.UseSqlServer(conn));
            services.AddDbContext<HangFireContext>(options => options.UseSqlServer(hangfireConn));
            services.AddScoped<IItem, itemRepository>();
            services.AddScoped<IMonthlies, MonthliesRepository>();
            services.AddTransient<RemindMe>();
            
            

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(hangfireConn, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "David Project", Version = "v1" });
            });   
        
            services.AddLogging(b =>
            {
                b.AddConfiguration(Configuration.GetSection("Logging")).AddConsole().AddDebug();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IBackgroundJobClient backgroundJobs)
        {
            logger.AddConsole(Configuration.GetSection("Logging"));
            logger.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "David Project v1");
                c.RoutePrefix = "";
            });
            
            app.UseHangfireDashboard();
             backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
            var taskss = task;
            RemindMe me = new RemindMe(taskss);
            RecurringJob.AddOrUpdate(() => me.Reminder(),Cron.Daily);
            RecurringJob.AddOrUpdate(() => Console.WriteLine(), Cron.Minutely);
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseMvcWithDefaultRoute();
        }
    }
}
