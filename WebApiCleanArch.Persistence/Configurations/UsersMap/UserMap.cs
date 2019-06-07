using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiCleanArch.Domain.Entities.Users;

namespace WebApiCleanArch.Persistence.Configurations.UsersMap
{
   public class UserMap:MyEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(c => c.UserName).IsRequired();
            builder.Property(c => c.PasswordHash).IsRequired();
            builder.Ignore(c => c.GenderType);
            base.Configure(builder);
        }
    }
}
