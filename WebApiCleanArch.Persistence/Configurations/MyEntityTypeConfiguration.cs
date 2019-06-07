using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiCleanArch.Domain.Entities.Base;

namespace WebApiCleanArch.Persistence.Configurations
{
    public class MyEntityTypeConfiguration<TEntity> : IMappingConfiguration, IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
    {
        public virtual void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TEntity> builder)
        {
            this.PostConfigure(builder);
        }

        protected virtual void PostConfigure(EntityTypeBuilder<TEntity> builder)
        {
        }

        public virtual void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }
    }

    public interface IMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}
