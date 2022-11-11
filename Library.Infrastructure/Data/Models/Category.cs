using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants.Category;

namespace Library.Infrastructure.Data.Models
{
    public class Category
    {
        public Category()
        {
            this.Books = new List<Book>();
        }
        
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxNameLength)]
        public string Name { get; set; } = null!;

        public List<Book> Books { get; set; }
    }
}
