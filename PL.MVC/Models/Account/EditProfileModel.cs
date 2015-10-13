using System.ComponentModel.DataAnnotations;

namespace PL.MVC.Models.Account
{
    public class EditProfileModel
    {
        [Required]
        [Display(Name = "Username")]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} and max {1} characters long.", MinimumLength = 4)]
        public string UserName { get; set; }

        [Display(Name = "Name")]
        [StringLength(32, ErrorMessage = "Maximum length of name is 64 characters.")]
        public string Name { get; set; }

        [Display(Name = "Bio")]
        [StringLength(512, ErrorMessage = "Maximum length of bio is 500 characters.")]
        [UIHint("Multilinetext")]
        public string Bio { get; set; }

        [Display(Name = "Website")]
        [DataType(DataType.Url, ErrorMessage = "Enter a valid website.")]
        public string Website { get; set; }
    }
}