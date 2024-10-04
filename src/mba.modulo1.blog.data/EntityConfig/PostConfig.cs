using mba.modulo1.blog.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MBA.Modulo1.Blog.Data.EntityConfig;

public class PostConfig : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");
        builder.HasKey(j => j.Id);
        builder.Property(j => j.Content).IsRequired().HasColumnType("varchar(1000)");
        builder.Property(j => j.Title).IsRequired().HasColumnType("varchar(50)");
        builder.Property(j => j.CreatedAt).IsRequired().HasColumnType("datetime");
        builder.Property(j => j.UpdatedAt).IsRequired().HasColumnType("datetime");
        builder.Property(j => j.AuthorId).IsRequired().HasColumnType("nvarchar(450)");

        builder
            .HasMany(j => j.Comments)
            .WithOne(j => j.Post)
            .HasForeignKey(j => j.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(j => j.User).WithMany(j => j.Posts).HasForeignKey(j => j.AuthorId);
    }
}