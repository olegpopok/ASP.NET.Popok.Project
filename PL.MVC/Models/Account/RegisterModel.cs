using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PL.MVC.Models.Account
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Username")]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} and max {1} characters long.", MinimumLength = 4)]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9]*$", ErrorMessage = "Username must be started with a letter and cannot contain symbols.")]
        public string UserName { get; set; }

        [Display(Name = "Name")]
        [StringLength(32, ErrorMessage = "Maximum length of {0} is {1} characters.")]
        public string Name { get; set; }

        [Display(Name = "Bio")]
        [StringLength(512, ErrorMessage = "Maximum length of bio is {1} characters.")]
        [UIHint("Multilinetext")]
        public string Bio { get; set; }

        [Display(Name = "Website")]
        [DataType(DataType.Url, ErrorMessage = "Enter a valid website.")]
        public string Website { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} and max {1} characters long.", MinimumLength = 4)]
        public string Password { get; set; }

        [Required]  
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Avatar")]
        public HttpPostedFileBase Avatar { get; set; }
    }
}