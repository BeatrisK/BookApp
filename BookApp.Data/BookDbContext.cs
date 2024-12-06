using BookApp.Data.Configuration;
using BookApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection;

namespace BookApp.Data
{
    public class BookDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
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

        public virtual DbSet<ReadList> ReadLists { get; set; }

        public virtual DbSet<WantToReadList> WantToReadLists { get; set; }

        public virtual DbSet<ApplicationUserBook> UsersBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
