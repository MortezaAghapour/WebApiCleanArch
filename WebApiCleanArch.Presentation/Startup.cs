using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApiCleanArch.Application.ViewModels.AppSettingViewModels;
using WebApiCleanArch.Domain.Interfaces.Repositories;
using WebApiCleanArch.Domain.Interfaces.Services.JwtServices;
using WebApiCleanArch.Infrastructure.Middlewares;
using WebApiCleanArch.Infrastructure.Services.JwtServices;
using WebApiCleanArch.Persistence.DbContexts;
using WebApiCleanArch.Persistence.Repositories;
using WebApiCleanArch.Presentation.Infrastructure.CustomExtensions;
using WebApiCleanArch.Presentation.Infrastructure.DatabaseHelper;

namespace WebApiCleanArch.Presentation
{
    public class Startup
    {
        private readonly JwtSetting _jwtSetting;
        private readonly IdentitySetting _identitySetting;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _jwtSetting = configuration.Get<JwtSetting>();
            _identitySetting = configuration.Get<IdentitySetting>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<JwtSetting>(options => Configuration.GetSection("JwtSettings").Bind(options));
            services.Configure<IdentitySetting>(options => Configuration.GetSection("IdentitySettings").Bind(options));








            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddCustomIdentity(_identitySetting);

            services.AddMinimalMvc();

            services.AddJwtAuthentication(_jwtSetting);
            return services.BuildAutofacServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            await SeedData.Initialize(app.ApplicationServices);
            //            if (env.IsDevelopment())
            //            {
            //                app.UseDeveloperExceptionPage();
            //            }
            //            else
            //            {
            //                app.UseExceptionHandler("/Home/Error");
            //                app.UseHsts();
            //            }

            app.UseCustomExceptionHandler();

            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseCookiePolicy();


        }
    }
}
