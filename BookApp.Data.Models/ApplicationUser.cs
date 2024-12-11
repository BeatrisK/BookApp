namespace BookApp.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public bool IsDeleted { get; set; }

        public virtual ICollection<ApplicationUserBook> ApplicationUserBooks { get; set; } =
               new HashSet<ApplicationUserBook>();
        public virtual ICollection<ReadList> ReadLists { get; set; }
            = new HashSet<ReadList>();
        public virtual ICollection<WantToReadList> WantToReadLists { get; set; }
            = new HashSet<WantToReadList>();
    }
    
}