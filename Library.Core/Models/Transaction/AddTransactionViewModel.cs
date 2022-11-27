using Ganss.Xss;
using System.ComponentModel.DataAnnotations;

namespace Library.Core.Models.Transaction
{
    public class AddTransactionViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Reciever Email is required.")]
        [Display(Name = "Username")]
        public string RecieverEmail { get; set; }

        [Display(Name = "Message")]
        public string? Message { get; set; }

        public string? SanitizedMessage => new HtmlSanitizer().Sanitize(this.Message);

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid positive number.")]
        [Required(ErrorMessage = "Please enter a valid number.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
    }
}
