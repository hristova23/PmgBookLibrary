using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Library.Data.DataConstants.Transaction;

namespace Library.Data.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(MaxMessageLength)]
        public string? Message { get; set; }

        [Required]
        public string SenderId { get; set; } = null!;
        
        [ForeignKey(nameof(SenderId))]
        public ApplicationUser Sender { get; set; } = null!;

        [Required]
        public string RecieverId { get; set; } = null!;

        [ForeignKey(nameof(RecieverId))]
        public ApplicationUser Reciever { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }
    }
}
