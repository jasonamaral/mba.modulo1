using mba.modulo1.blog.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace mba.modulo1.blog.data.EntityConfig;

public class CommentConfig : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");
        builder.HasKey(j => j.Id);
        builder.Property(j => j.Content).IsRequired().HasColumnType("varchar(1000)");
        builder.Property(j => j.PostId).IsRequired().HasColumnType("uniqueidentifier");
        builder.Property(j => j.CreatedAt).IsRequired().HasColumnType("datetime");
        builder.Property(j => j.UpdatedAt).IsRequired().HasColumnType("datetime");
        builder.Property(j => j.AuthorId).IsRequired().HasColumnType("nvarchar(450)");

        builder
            .HasOne(j => j.User)
            .WithMany(j => j.Comments)
            .HasForeignKey(j => j.AuthorId);
    }
}