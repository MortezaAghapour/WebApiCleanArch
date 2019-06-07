using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiCleanArch.Presentation.Infrastructure.CustomExtensions
{
    public static class StartupExtensions
    {
        public static void AddMinimalMvc(this IServiceCollection services)
        {
            //https://github.com/aspnet/Mvc/blob/release/2.2/src/Microsoft.AspNetCore.Mvc/MvcServiceCollectionExtensions.cs
            services.AddMvcCore(options =>
                {
                    options.Filters.Add(new AuthorizeFilter());

                    //Like [ValidateAntiforgeryToken] attribute but dose not validatie for GET and HEAD http method
                    //You can ingore validate by using [IgnoreAntiforgeryToken] attribute
                    //Use this filter when use cookie 
                    //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

                    //options.UseYeKeModelBinder();
                })
                .AddApiExplorer()
                .AddAuthorization()
                .AddFormatterMappings()
                .AddDataAnnotations()
                .AddJsonFormatters(/*options =>
            {
                options.Formatting = Newtonsoft.Json.Formatting.Indented;
                options.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            }*/)
                .AddCors()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
    }
}
