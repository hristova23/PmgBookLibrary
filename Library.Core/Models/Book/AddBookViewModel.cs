using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants.Book;

namespace Library.Core.Models.Book
{
    public class AddBookViewModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(PublisherMaxLength, MinimumLength = PublisherMinLength)]
        [Display(Name = "Publisher")]
        public string Publisher { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
