using System.ComponentModel.DataAnnotations;

namespace Library.Data.Models
{
    public class BookLanguage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string LanguageName { get; set; } = null!;

        [Required]
        public string LanguageCode { get; set; } = null!;
    }
}
