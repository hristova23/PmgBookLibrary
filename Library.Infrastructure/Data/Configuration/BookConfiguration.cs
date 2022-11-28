using Library.Infrastructure.Data.Models;
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
                   Id = 1,
                   Title = "Crack The Code",
                   PublisherId = "dea12856-c198-4129-b3f3-b893d8395082",
                   ImageUrl = "crackthecode.jpeg",
                   PdfUrl = "CrackTheCode.pdf",
                   Description = "Simon Singh brings life to an amazing story of puzzles, codes, languages and riddles – revealing the continual pursuit to disguise and uncover, and to work out the secret languages of others. Codes have influenced events throughout history, both in the stories of those who make them and those who break them. ",
                   CategoryId = 6
                },
                new Book()
                {
                   Id = 2,
                   Title = "Harry Potter",
                   PublisherId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                   ImageUrl = "Harry_Potter.jpg",
                   PdfUrl = "HarryPotter.pdf",
                   Description = "It is a story about Harry Potter, an orphan brought up by his aunt and uncle because his parents were killed when he was a baby. Harry is unloved by his uncle and aunt but everything changes when he is invited to join Hogwarts School of Witchcraft and Wizardry and he finds out he's a wizard",
                   CategoryId = 5
                }
            };

            return books;
        }
    }
}
