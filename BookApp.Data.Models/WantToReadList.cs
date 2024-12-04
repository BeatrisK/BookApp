namespace BookApp.Data.Models
{
    public class WantToReadList
    {
        public int Id { get; set; }  

        public int UserId { get; set; }  


        public ApplicationUser User { get; set; } = null!;

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}
