namespace BookApp.Web.ViewModels.MyBook
{
    public class IndexMyBooViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public bool IsInReadList { get; set; }

        public bool IsInWantToReadList { get; set; }
    }
}
