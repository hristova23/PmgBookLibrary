using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Library.Infastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Books = new List<Book>();
            this.SendedTransactions = new List<Transaction>();
            this.RecievedTransactions = new List<Transaction>();
            this.Credits = 0;
        }

        public List<Book> Books { get; set; } 

        public List<Transaction> SendedTransactions { get; set; }

        public List<Transaction> RecievedTransactions { get; set; }

        [Required]
        public int Credits { get; set; }
    }
}
