using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebApiCleanArch.Application.ViewModels.AppSettingViewModels;
using WebApiCleanArch.Domain.Entities.Users;
using WebApiCleanArch.Persistence.DbContexts;

namespace WebApiCleanArch.Presentation.Infrastructure.CustomExtensions
{
    public static class IdentityExtentions
    {

        public static void AddCustomIdentity(this IServiceCollection services, IdentitySetting settings)
        {
            services.AddIdentity<User, Role>(identityOptions =>
                {
                    //Password Settings
                    identityOptions.Password.RequireDigit = settings.PasswordRequireDigit;
                    identityOptions.Password.RequiredLength = settings.PasswordRequiredLength;
                    identityOptions.Password.RequireNonAlphanumeric = settings.PasswordRequireNonAlphanumic; //#@!
                    identityOptions.Password.RequireUppercase = settings.PasswordRequireUppercase;
                    identityOptions.Password.RequireLowercase = settings.PasswordRequireLowercase;

                    //UserName Settings
                    identityOptions.User.RequireUniqueEmail = settings.RequireUniqueEmail;

                    //Singin Settings
                    //identityOptions.SignIn.RequireConfirmedEmail = false;
                    //identityOptions.SignIn.RequireConfirmedPhoneNumber = false;

                    //Lockout Settings
                    //identityOptions.Lockout.MaxFailedAccessAttempts = 5;
                    //identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    //identityOptions.Lockout.AllowedForNewUsers = false;
                })
                .AddEntityFrameworkStores<MyDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
