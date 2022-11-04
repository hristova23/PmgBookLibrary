using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants.Transaction;

namespace Library.Data.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(MaxMessageLength)]
        public string? Message { get; set; }

        [Required]
        public string SenderId { get; set; } = null!;
        public ApplicationUser Sender { get; set; } = null!;

        [Required]
        public string RecieverId { get; set; } = null!;
        public ApplicationUser Reciever { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }
    }
}
