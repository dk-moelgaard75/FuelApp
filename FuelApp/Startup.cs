using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FuelApp.Data;
using FuelApp.Options;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FuelApp
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMemoryCache(); //adds caching for session values
            services.AddSession(obj => obj.IdleTimeout = TimeSpan.FromMinutes(30)); //adds session capabilities

            services.Configure<EmailServiceOptions>(_configuration.GetSection("Email")); //get section from appsettings.json
            //DI emailservice
            services.AddSingleton<IEmailService, EmailService>(); //Adds emailservice

            //Get SQL/EF connection info and setup configuration as singleton
            var conString = _configuration.GetConnectionString("DefaultConnection");
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<Data.FuelAppDbContext>((serviceProvider, options) =>
                options.UseSqlServer(conString)
                .UseInternalServiceProvider(serviceProvider)
              );

            //Adding EF configuration 
            var appContextOptionbuilder = new DbContextOptionsBuilder<FuelAppDbContext>().UseSqlServer(conString);
            services.AddSingleton(appContextOptionbuilder.Options);
            
            //Secure App with authentication thru cookie
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/UserRegistration/ErrorDenied";
                    options.LoginPath = "/UserRegistration/Login";
                });

            //Add services for DI
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IVehicleService, VehicleService>();
            services.AddSingleton<IFuelingService, FuelingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //Ensures using .html og .js files
            app.UseStaticFiles();

            //Use session for sessionvariables
            app.UseSession();

            //Use authentication to protect part of the App
            app.UseAuthentication();

            //Adding default route
            app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });
        }
    }
}
