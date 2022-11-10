using Library.Infastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Data.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(SeedCategories());
        }

        private List<Category> SeedCategories()
        {
            List<Category> categories = new List<Category>()
            {
               new Category()
               {
                   Id = 1,
                   Name = "Action"
               },
               new Category()
               {
                   Id = 2,
                   Name = "Biography"
               },
               new Category()
               {
                   Id = 3,
                   Name = "Children"
               },
               new Category()
               {
                   Id = 4,
                   Name = "Crime"
               },
               new Category()
               {
                   Id = 5,
                   Name = "Fantasy"
               }
            };  

            return categories;
        }
    }
}
