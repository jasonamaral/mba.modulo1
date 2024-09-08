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

        builder.HasMany(j => j.Comments)
        .WithOne(j => j.Post)
        .HasForeignKey(j => j.PostId);

        // Configurações adicionais se necessário

        // Relacionamento entre Post e ApplicationUser
        //builder.Entity<Post>()
        //    .HasOne(p => p.Author)
        //    .WithMany(u => u.Posts)
        //    .HasForeignKey(p => p.AuthorId);

        // Relacionamento entre Comment e ApplicationUser
        //builder.Entity<Comment>()
        //    .HasOne(c => c.Author)
        //    .WithMany(u => u.Comments)
        //    .HasForeignKey(c => c.AuthorId);
    }
}