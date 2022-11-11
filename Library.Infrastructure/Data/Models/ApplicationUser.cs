using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Library.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.FinishedBooks = new List<FinishedBook>();
            this.FavoriteBooks = new List<FavoriteBook>();
            this.SendedTransactions = new List<Transaction>();
            this.RecievedTransactions = new List<Transaction>();
            this.Credits = 0;
        }

        public List<FinishedBook> FinishedBooks { get; set; } 

        public List<FavoriteBook> FavoriteBooks { get; set; } 

        public List<Transaction> SendedTransactions { get; set; }

        public List<Transaction> RecievedTransactions { get; set; }

        [Required]
        public int Credits { get; set; }
    }
}
