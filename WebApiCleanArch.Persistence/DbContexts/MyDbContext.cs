using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiCleanArch.Common.Extensions;
using WebApiCleanArch.Domain.Entities.Base;
using WebApiCleanArch.Domain.Entities.Users;
using WebApiCleanArch.Persistence.Extensions;

namespace WebApiCleanArch.Persistence.DbContexts
{
    public class MyDbContext : IdentityDbContext<User,Role,int>
    {

        #region Constrcutors

        public MyDbContext(DbContextOptions options) : base(options)
        {

        }
        public MyDbContext()
        {

        }
        #endregion


        #region Overrides

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assemblies = typeof(IEntity).Assembly;
            //register all entities , no need DbSet<T>
            modelBuilder.RegisterAllEntities<IEntity>(assemblies);
            //register all IEntityTypeConfiguration
            modelBuilder.RegisterConfigurations();
            
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            //pluralize table names
            modelBuilder.PluralizingTableNameConvention();
        }

        #endregion

        #region Save Change Override 

        public override int SaveChanges()
        {
            this.ChangeTracker.CleanString();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ChangeTracker.CleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            this.ChangeTracker.CleanString();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            this.ChangeTracker.CleanString();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        #endregion

    }
}
