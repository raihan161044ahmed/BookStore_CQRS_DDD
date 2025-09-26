using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookStore.Domain.Entities;


namespace BookStore.Infrastructure.Persistence.Configurations;


public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");
        builder.HasKey(b => b.Id);


        builder.Property(b => b.Title).IsRequired().HasMaxLength(256);
        builder.Property(b => b.ISBN).HasMaxLength(50);
        builder.Property(b => b.Description).HasMaxLength(4000);
        builder.Property(b => b.Price).HasColumnType("decimal(18,2)");


        builder.HasOne(b => b.Author)
        .WithMany(a => a.Books)
        .HasForeignKey(b => b.AuthorId)
        .OnDelete(DeleteBehavior.SetNull);
    }
}