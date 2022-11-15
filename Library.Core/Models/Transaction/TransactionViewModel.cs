using Ganss.Xss;
using System.ComponentModel.DataAnnotations;

namespace Library.Core.Models.Transaction
{
    public class TransactionViewModel
    {
        public int Id { get; set; }

        public string SenderId { get; set; }

        public string SenderUsername { get; set; }

        public string RecieverId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "Username")]
        public string RecieverUsername { get; set; }

        public string? Message { get; set; }
        public string SanitizedMessage => new HtmlSanitizer().Sanitize(this.Message);

        [Range(1, 1000, ErrorMessage = "Please enter a number between 0 and 1000.")]//=> data constants
        [Required(ErrorMessage = "Please enter a valid number.")]
        [DataType(DataType.Currency)]
        public int Quantity { get; set; }

        public DateTime Date { get; set; }
    }
}
