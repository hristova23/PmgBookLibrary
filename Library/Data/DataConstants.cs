﻿namespace Library.Data
{
    public class DataConstants
    {
        public class Category
        {
            public const int MinNameLength = 5;
            public const int MaxNameLength = 50;
        }

        public class Book
        {
            public const int TitleMinLength = 10;
            public const int TitleMaxLength = 50;

            public const int AuthorMinLength = 5;
            public const int AuthorMaxLength = 50;

            public const int DescriptionMinLength = 5;
            public const int DescriptionMaxLength = 5000;
        }
    }
}
