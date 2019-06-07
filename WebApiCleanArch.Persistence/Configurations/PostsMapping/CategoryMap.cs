using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiCleanArch.Domain.Entities.Posts;

namespace WebApiCleanArch.Persistence.Configurations.PostsMapping
{
   public class CategoryMap:MyEntityTypeConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired();
            builder.HasOne(c => c.Parent).WithMany(c => c.Categories).HasForeignKey(c => c.ParentId);
            base.Configure(builder);
        }
    }
}
