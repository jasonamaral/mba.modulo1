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
        builder.Property(j => j.PostId).IsRequired().HasColumnType("varchar(50)");
        builder.Property(j => j.CreatedAt).IsRequired().HasColumnType("datetime");
        builder.Property(j => j.UpdatedAt).IsRequired().HasColumnType("datetime");
    }
}