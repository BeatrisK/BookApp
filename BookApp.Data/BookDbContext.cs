using BookApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Data
{
    public class BookDbContext : IdentityDbContext<IdentityUser>
    {
        public BookDbContext()
        {
            
        }

        public BookDbContext(DbContextOptions<BookDbContext> options)
            :base(options)
        {
            
        }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<Shelve> Shelves { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
