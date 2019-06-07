using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApiCleanArch.Persistence.Configurations;

namespace WebApiCleanArch.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        #region Register Fluent Api
        public static void RegisterConfigurations(this ModelBuilder builder)
        {
            var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                (type.BaseType?.IsGenericType ?? false)
                && type.BaseType.GetGenericTypeDefinition() == typeof(MyEntityTypeConfiguration<>));

            foreach (var typeConfiguration in typeConfigurations)
            {
                var configuration = (IMappingConfiguration)Activator.CreateInstance(typeConfiguration);
                configuration.ApplyConfiguration(builder);
            }

        }
        #endregion
    }
}
