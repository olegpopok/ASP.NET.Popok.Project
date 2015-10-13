using System.ComponentModel.DataAnnotations;

namespace PL.MVC.Models.Account
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Username")]
        [StringLength(32, ErrorMessage = "Maximum length of username is 32 characters.")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(64, ErrorMessage = " Maximum length of password is 64 characters.")]
        [MinLength(4, ErrorMessage = "Minimum length of password is four charecters.")]
        public string Password { get; set; }

        [Display(Name = "Remember?")]
        public bool Remember { get; set; }
    }
}