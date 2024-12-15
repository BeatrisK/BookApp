namespace BookApp.Common
{
    public static class EntityValidationMessages
    {
        public static class Book
        {
            public const string TitleRequiredMessage = "Book title is required.";
            public const string GenreRequiredMessage = "Genre is required.";
            public const string DescriptionRequiredMessage = "Description is required.";
            public const string PublisherRequiredMessage = "Publisher is required.";
            public const string PriceRequiredMessage = "Please specify book price.";
            public const string AuthorNameRequiredMessage = "Author name is required.";
        }
    }
}
