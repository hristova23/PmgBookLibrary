using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants.Book;

namespace Library.Core.Models.Book
{
    public class UploadBookViewModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Image")]
        public IFormFile Image { get; set; }

        [Required]
        [Display(Name = "Pdf")]
        public IFormFile Pdf { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
