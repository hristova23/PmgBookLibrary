using Library.Infastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Data.Configuration
{
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(SeedBooks());
        }

        private List<Book> SeedBooks()
        {
            var books = new List<Book>()
            {
                new Book()
                {
                   Id = 5,
                   Title = "Lorem Ipsum",
                   AuthorId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                   ImageUrl = "https://img.freepik.com/free-psd/book-cover-mock-up-arrangement_23-2148622888.jpg?w=826&t=st=1666106877~exp=1666107477~hmac=5dea3e5634804683bccfebeffdbde98371db37bc2d1a208f074292c862775e1b",
                   Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                   CategoryId = 1,
                   Rating = 9.5m
                }
            };

            return books;
        }
    }
}
