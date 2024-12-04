using BookApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookApp.Data.Configuration
{
    public class ApplicationUserBookConfiguration : IEntityTypeConfiguration<ApplicationUserBook>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserBook> builder)
        {
            builder
                .HasKey(ub => new
                { ub.ApplicationUserId, ub.BookId });

            builder
                .HasOne(ub => ub.Book)
                .WithMany(b => b.BookApplicationUsers)
                .HasForeignKey(b => b.BookId);

            builder
                .HasOne(ub => ub.ApplicationUser)
                .WithMany(u => u.ApplicationUserBooks)
                .HasForeignKey(ub => ub.ApplicationUserId);
        }
    }
}
