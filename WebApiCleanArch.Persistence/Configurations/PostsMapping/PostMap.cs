using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiCleanArch.Domain.Entities.Posts;

namespace WebApiCleanArch.Persistence.Configurations.PostsMapping
{
   public class PostMap:MyEntityTypeConfiguration<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title).IsRequired();
            builder.Property(c => c.Description).IsRequired();
            builder.HasOne(c => c.Category).WithMany(c => c.Posts).HasForeignKey(c => c.CategoryId);
            builder.HasOne(c => c.Author).WithMany(c => c.Posts).HasForeignKey(c => c.AuthorId);
            base.Configure(builder);
        }
    }
}
