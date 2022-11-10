using System.ComponentModel.DataAnnotations;

namespace Library.Core.Models.User
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]//use data constants class
        [Display(Name = "Username")]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(60, MinimumLength = 10)]//use data constants class
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 5)]//use data constants class
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
