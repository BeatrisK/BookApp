namespace BookApp.Data.Configuration
{
    using BookApp.Data.Models;
    using static Common.EntityValidationConstants.Book;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .HasKey(b => b.Id);

            builder
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(TitleMaxLength);

            builder
                .Property(b => b.Genre)
                .IsRequired()
                .HasMaxLength(GenreMaxLength);

            builder
                .Property(b => b.Pages)
                .IsRequired();

            builder
                .Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            builder
                .Property(b => b.Publisher)
                .IsRequired()
                .HasMaxLength(PublisherMaxLength);

            builder
                .Property(b => b.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder
                .Property(m => m.ImageUrl)
                .IsRequired(false)
                .HasMaxLength(ImageUrlMaxLength);

            builder
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(b => b.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
