using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApiCleanArch.Persistence.DbContexts;

namespace WebApiCleanArch.Presentation.Infrastructure.DatabaseHelper
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider provider)
        {
            //create samples
            using (var serviceScope = provider.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<MyDbContext>();

                // Seed the database.
                await InitializeData(context);
            }


        }

        private static async Task InitializeData(MyDbContext context)
        {

            context.Database.EnsureCreated();



            //این باید بعد 
            //ensurecreated
            // بیاد چون 
            //ensurecreated
            //باعث ایجاد دیتابیس میشود و 
            //migrate
            //بایث ایجاد جداول و اگر جلوتر بیاد باعث خطای زیر میشود
            //invalid object name X
            context.Database.Migrate();



        }
    }
}
