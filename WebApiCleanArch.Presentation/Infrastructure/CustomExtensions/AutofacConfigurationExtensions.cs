using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApiCleanArch.Application.ViewModels.ApiResultViewModels;
using WebApiCleanArch.Application.ViewModels.AppSettingViewModels;
using WebApiCleanArch.Common.ConstStrings;
using WebApiCleanArch.Domain.Entities.Base;
using WebApiCleanArch.Domain.Interfaces.GeneralIntefaces;
using WebApiCleanArch.Domain.Interfaces.Repositories;
using WebApiCleanArch.Domain.Interfaces.Services.JwtServices;
using WebApiCleanArch.Infrastructure.FilterAttributes;
using WebApiCleanArch.Infrastructure.Services.JwtServices;
using WebApiCleanArch.Persistence.DbContexts;
using WebApiCleanArch.Persistence.Repositories;
using WebApiCleanArch.Presentation.Infrastructure.DatabaseHelper;

namespace WebApiCleanArch.Presentation.Infrastructure.CustomExtensions
{
    public static class AutofacConfigurationExtensions
    {
        public static void AddServices(this ContainerBuilder containerBuilder)
        {
            //RegisterType > As > Liftetime
            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            containerBuilder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            containerBuilder.RegisterType<ActionContextAccessor>().As<IActionContextAccessor>().SingleInstance();


            var commonAssembly = typeof(Resource).Assembly;
            var entitiesAssembly = typeof(IEntity).Assembly;
            var domainAssembly = typeof(IEntity).Assembly;
            var applicationAssembly = typeof(ApiResult).Assembly;
            var infrastructureAssembly = typeof(ApiResultAttribute).Assembly;

            var persistenceAssembly = typeof(MyDbContext).Assembly;
            var presentationAssembly = typeof(SeedData).Assembly;



            containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, domainAssembly, applicationAssembly, infrastructureAssembly, persistenceAssembly, presentationAssembly)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, domainAssembly, applicationAssembly, infrastructureAssembly, persistenceAssembly, presentationAssembly)
                .AssignableTo<ITransientDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, domainAssembly, applicationAssembly, infrastructureAssembly, persistenceAssembly, presentationAssembly)
                .AssignableTo<ISingletonDependency>()
                .AsImplementedInterfaces()
                .SingleInstance();

        }

        public static IServiceProvider BuildAutofacServiceProvider(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            //Register Services to Autofac ContainerBuilder
            containerBuilder.AddServices();
      
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}
