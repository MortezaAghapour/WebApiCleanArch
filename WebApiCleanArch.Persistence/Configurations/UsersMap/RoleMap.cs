using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiCleanArch.Domain.Entities.Users;

namespace WebApiCleanArch.Persistence.Configurations.UsersMap
{
    public class RoleMap:MyEntityTypeConfiguration<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(c => c.Name).IsRequired();
            base.Configure(builder);
        }
    }
}
