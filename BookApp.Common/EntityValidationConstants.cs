namespace BookApp.Common
{
    public class EntityValidationConstants
    {
        public static class Book
        {
            public const int TitleMaxLength = 50;
            public const int TitleMinLength = 3;
            public const int GenreMinLength = 5;
            public const int GenreMaxLength = 20;
            public const int DescriptionMaxLength = 500;
            public const int DescriptionMinLength = 50;
            public const int PublisherMaxLength = 20;
            public const int PublisherMinLength = 5;
            public const int ImageUrlMaxLength = 2083;
            public const int ImageUrlMinLength = 8;
        }

        public static class Author
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 5;
        }

        public static class Review
        {
            public const int RatingMaxValue = 5;
            public const int RatingMinValue = 1;
            public const string ReviewDateFormat = "MM/yyyy";
            public const int ReviewTextMaxLength = 2000;
            public const int ReviewTextMinLength = 50;
        }
    }
}
