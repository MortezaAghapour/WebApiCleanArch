using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Pluralize.NET.Core;

namespace WebApiCleanArch.Common.Extensions
{
  public static  class ModelBuilderExtension
    {

        public static void SingularizingTableNameConvention(this ModelBuilder  builder)
        {
            var pluralizer = new Pluralizer();
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.Relational().TableName;
                entityType.Relational().TableName =tableName.Singularize();
            }
        }
        public static void PluralizingTableNameConvention(this ModelBuilder  builder)
        {
            var pluralizer = new Pluralizer();
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.Relational().TableName;
                entityType.Relational().TableName = tableName.Pluralize();
            }
        }


        public static void RegisterEntityTypeConfiguration(this ModelBuilder builder, params Assembly[] assemblies)
        {
            var applyGenericMethod = typeof(ModelBuilder).GetMethods().First(m => m.Name == nameof(ModelBuilder.ApplyConfiguration));

            var types = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic);

            foreach (var type in types)
            {
                foreach (var iface in type.GetInterfaces())
                {
                    var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(iface.GenericTypeArguments[0]);
                    if (iface.IsConstructedGenericType && iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    {
                        applyConcreteMethod.Invoke(builder, new object[] { Activator.CreateInstance(type) });
                    }
                }
            }
        }
        public static void RegisterEntityTypeConfigurations(this ModelBuilder builder, Assembly assembly)
        {
 

            var applyGenericMethod = typeof(ModelBuilder).GetMethods()
                .First(c => c.Name == nameof(ModelBuilder.ApplyConfiguration));

            var types = assembly.DefinedTypes.Where(t =>
                    t.ImplementedInterfaces.Any(i =>
                        i.IsGenericType &&
                        i.Name.Equals(typeof(IEntityTypeConfiguration<>).Name,
                            StringComparison.InvariantCultureIgnoreCase)
                    ) &&
                    t.IsClass &&
                    !t.IsAbstract &&
                    !t.IsNested)
                .ToList();
            foreach (var type in types)
            {
                foreach (var iface in type.GetInterfaces())
                {
                    if (iface.IsConstructedGenericType &&
                        iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    {
                        var concreteMethod = applyGenericMethod.MakeGenericMethod(iface.GenericTypeArguments[0]);
                        concreteMethod.Invoke(builder, new object[] { Activator.CreateInstance(type) });
                    }

                }
            }
        }

        public static void RegisterAllEntities<TBaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(TBaseType).IsAssignableFrom(c));

            foreach (var type in types)
                modelBuilder.Entity(type);
        }
        public static void AddRestrictDeleteBehaviorConvention(this ModelBuilder builder)
        {
            var cascadeFKs = builder.Model.GetEntityTypes().SelectMany(c => c.GetForeignKeys())
                .Where(c => !c.IsOwnership && c.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var item in cascadeFKs)
            {
                item.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }


        public static void AddSequentialGuidForIdConvention(this ModelBuilder builder)
        {
            builder.AddDefaultValueSqlConvention("Id", typeof(Guid), "NEWSEQUENTIALID()");
        }

        public static void AddDefaultValueSqlConvention(this ModelBuilder builder, string propertyName, Type type, string defaultValue)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                var property = entity.GetProperties().SingleOrDefault(c =>
                    c.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
                if (property != null && property.ClrType == type)
                {
                    property.Relational().DefaultValueSql = defaultValue;
                }
            }
        }


    }
}
