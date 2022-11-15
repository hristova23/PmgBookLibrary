using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Models.Transaction
{
    public class AddTransactionViewModel
    {
        [Required]
        //[StringLength(TransactionMaxLength, MinimumLength = TransactionMinLength)]
        [Display(Name = "Reciever Username")]
        public string RecieverUsername { get; set; } = null!;

        [Required]
        //[maxlength(TransactionMaxLength, MinimumLength = TransactionMinLength)]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Message")]
        public string? Message { get; set; }
    }
}
