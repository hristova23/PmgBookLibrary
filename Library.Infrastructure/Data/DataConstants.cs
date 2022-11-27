namespace Library.Data
{
    public class DataConstants
    {
        public class Category
        {
            public const int MinNameLength = 4;
            public const int MaxNameLength = 50;
        }

        public class Book
        {
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 50;

            public const int PublisherMinLength = 5;
            public const int PublisherMaxLength = 50;

            public const int DescriptionMinLength = 5;
            public const int DescriptionMaxLength = 5000;
        }

        public class Transaction
        {
            public const int MaxMessageLength = 5000;
        }
    }
}
