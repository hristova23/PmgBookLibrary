using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Library.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<ApplicationUserBook> ApplicationUsersBooks { get; set; } = new List<ApplicationUserBook>();

        public ICollection<Transaction> SendedTransactions { get; set; } = new List<Transaction>();

        public ICollection<Transaction> RecievedTransactions { get; set; } = new List<Transaction>();

        [Required]
        public int Credits { get; set; } = 0;
    }
}
