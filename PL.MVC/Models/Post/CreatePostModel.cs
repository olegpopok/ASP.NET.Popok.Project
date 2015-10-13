using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace PL.MVC.Models.Post
{
    public class CreatePostModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid AuthorId { get; set; }

        [StringLength(512, ErrorMessage = "Maximum length is {1} characters.")]
        [UIHint("Multilinetext")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Photo")]
        public HttpPostedFileBase Photo { get; set; }

    }
}