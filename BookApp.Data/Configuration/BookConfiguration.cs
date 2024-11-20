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

            //builder.HasData(this.GenerateBooks());
        }

        private IEnumerable<Book> GenerateBooks()
        {
            IEnumerable<Book> books = new List<Book>()
            {
                new Book()
                {
                    Title = "It Ends with Us",
                    Genre = "Romance",
                    Pages = 384,
                    Description = "23-year-old college graduate Lily Bloom moves to Boston with hopes of opening her own floral shop. She grew up in a broken family – her late father, Andrew, would physically abuse her mother, Jenny, leading Lily to resent him, as well as resenting her mother for staying with him. She has recently given a eulogy at Andrew's funeral in her hometown of Plethora, Maine, during which she announced she would list five things she loved the most about him, then stood in silence for several seconds before walking off.",
                    Publisher = "Atria Books",
                    Price = 9.00m,
                    ImageUrl = "https://www.ciela.com/media/catalog/product/cache/9a7ceae8a5abbd0253425b80f9ef99a5/i/t/it-ends-with-us-colleen-hoover-simon-and-schuster-uk_1.jpg",
                    AuthorId = 1
                },
                new Book()
                {
                    Title = "Ugly love",
                    Genre = "Romance",
                    Pages = 352,
                    Description = "When Tate Collins meets airline pilot Miles Archer, she knows it isn’t love at first sight. They wouldn’t even go so far as to consider themselves friends. The only thing Tate and Miles have in common is an undeniable mutual attraction. Once their desires are out in the open, they realize they have the perfect set-up. He doesn’t want love, she doesn’t have time for love, so that just leaves the sex. Their arrangement could be surprisingly seamless, as long as Tate can stick to the only two rules Miles has for her.",
                    Publisher = "Atria Books",
                    Price = 11.00m,
                    ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/u/g/f5d63fddf001954bdcd386b66cdf5714/ugly-love-30.jpg",
                    AuthorId = 1
                },
                new Book()
                {
                    Title = "Wuthering Heights",
                    Genre = "Gothic",
                    Pages = 352,
                    Description = "In 1801, Mr Lockwood, the new tenant at Thrushcross Grange in Yorkshire, pays a visit to his landlord, Heathcliff, at his remote moorland farmhouse, Wuthering Heights. There he meets a reserved young woman (later identified as Cathy Linton), Joseph, a cantankerous servant, and Hareton, an uneducated young man who speaks like a servant.",
                    Publisher = "Alma Classics",
                    Price = 6.00m,
                    ImageUrl = "https://cdn.ozone.bg/media/catalog/product/w/u/wuthering_heights_1666888058_0.jpg",
                    AuthorId = 2
                },
                new Book()
                {
                    Title = "Pride and Prejudice",
                    Genre = "Classic Regency novel",
                    Pages = 320,
                    Description = "In the early 19th century, the Bennet family live at their Longbourn estate, situated near the village of Meryton in Hertfordshire, England. Mrs Bennet's greatest desire is to marry off her five daughters to secure their futures.\r\n\r\nThe arrival of Mr Bingley, a rich bachelor who rents the neighbouring Netherfield estate, gives her hope that one of her daughters might contract a marriage to the advantage, because \"It is a truth universally acknowledged, that a single man in possession of a good fortune, must be in want of a wife\".",
                    Publisher = "Alma Classics",
                    Price = 8.00m,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQgQUKLzpivNwtzB_3Ucb95fn4eJRdNZ05Q1w&s",
                    AuthorId = 3
                }
            };

            return books;
        }
    }
}
