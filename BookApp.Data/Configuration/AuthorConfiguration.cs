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

            //builder
            //    .HasData(this.GenerateAuthors());
        }

        /*private IEnumerable<Author> GenerateAuthors()
        {
            IEnumerable<Author> authors = new List<Author>()
            {
                new Author()
                {
                    Name = "Jane Austen",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTyoIPawBe4km5iH9E3MLE4NjjRyLopnAg35Q&s"
                },
                new Author()
                {
                    Name = "Colleen Hoover",
                    ImageUrl = "https://static.wikia.nocookie.net/colleen_hoover/images/9/98/Colleenhoover.jpg/revision/latest/thumbnail/width/360/height/360?cb=20200525030700"
                },
                new Author()
                {
                    Name = "Emily Brontë",
                    ImageUrl = "https://ebbg7broonn.exactdn.com/wp-content/uploads/2021/01/Emily-Bronte-Portrait.jpg?strip=all&lossy=1&ssl=1"
                }
            };

            return authors;
        }*/
    }
}
