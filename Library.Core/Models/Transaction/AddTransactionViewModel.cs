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

        [Range(1, 1000, ErrorMessage = "Please enter a number between 0 and 1000.")]//=> data constants
        [Required(ErrorMessage = "Please enter a valid number.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
    }
}
