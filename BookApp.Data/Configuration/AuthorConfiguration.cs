namespace BookApp.Data.Configuration
{
    using BookApp.Data.Models;
    using static Common.EntityValidationConstants.Author;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
