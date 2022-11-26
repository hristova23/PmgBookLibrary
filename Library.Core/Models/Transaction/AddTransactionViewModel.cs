using Ganss.Xss;
using System.ComponentModel.DataAnnotations;

namespace Library.Core.Models.Transaction
{
    public class AddTransactionViewModel
    {
        public int Id { get; set; }

        public string SenderId { get; set; }

        [Required(ErrorMessage = "Sender Email is required.")]
        public string SenderEmail { get; set; }

        public string RecieverId { get; set; }

        [Required(ErrorMessage = "Reciever Email is required.")]
        [Display(Name = "Username")]
        public string RecieverEmail { get; set; }

        [Display(Name = "Message")]
        public string? Message
        {
            get
            {
                return new HtmlSanitizer().Sanitize(this.Message);
            }
            set
            {
                this.Message = value;
            }
        }

        [Range(1, 1000, ErrorMessage = "Please enter a number between 0 and 1000.")]//=> data constants
        [Required(ErrorMessage = "Please enter a valid number.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        public DateTime Date { get; set; }
    }
}
