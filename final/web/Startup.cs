using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.AzureAppServices.Internal;
using web.Controllers;
using web.Data;
using web.Data.Entities;
using web.Models;
using web.Services;

namespace web
{
    public class Startup
    {
        private IConfiguration Configuration;

        public Startup()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            Configuration = configBuilder.Build();
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
                });

            services.AddIdentity<Data.Entities.ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            
            services.AddTransient<IMailService, EMailService>(); 
            services.AddSingleton<IEmailConfiguration>(
                Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ShoppingCartModel>(sp => ShoppingCartModel.GetCart(sp));
            services.AddTransient<IKioskRepository, KioskRepository>(); 
            
            services.AddMvc();

            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Catch error and show the user a friendly error page
                app.UseExceptionHandler("/error");
            }
            
            app.UseSession();
            app.UseStaticFiles();
            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "categoryfilter",
                    template: "Product/{action}/{category?}",
                    defaults: new {Controller = "Product", action = "List"});
                
                routes.MapRoute(
                    name: "statcountfilter",
                    template: "Statistic/{action}/{count?}",
                    defaults: new {Controller = "Statistic", action = "List"});
                
                routes.MapRoute(
                    name: "emailcountfilter",
                    template: "Email/{action}/{count?}",
                    defaults: new {Controller = "Email", action = "Create"});
                
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
