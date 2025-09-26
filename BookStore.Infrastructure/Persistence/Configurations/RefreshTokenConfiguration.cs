using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookStore.Domain.Entities;


namespace BookStore.Infrastructure.Persistence.Configurations;


public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.HasKey(rt => rt.Id);
        builder.Property(rt => rt.Token).IsRequired().HasMaxLength(512);
        builder.HasOne<User>()
        .WithMany(u => u.RefreshTokens)
        .HasForeignKey(rt => rt.UserId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}