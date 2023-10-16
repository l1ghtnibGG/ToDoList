using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using ToDoList_Mvc_UI.Models;
using ToDoList_Mvc_UI.Models.Repo;

namespace ToDoList_Mvc_UI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ToDoListDbContext>(opts =>
            {
                opts.UseSqlServer(Configuration.GetConnectionString("ToDoListConnection"));
                opts.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<IToDoListRepository, EFToDoListRepository>();
            services.AddScoped<IUserRepository, EFUserRepository>();
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("editPage",
                    "Edit/{id:long}",
                    new { controller = "Home", action = "EditPage", id = 0 });
                endpoints.MapControllerRoute("default",
                    "Login",
                    new {controller="Home", action="Login", id = 0});
            });

            SeedData.EnsureData(app);
        }
    }
}
