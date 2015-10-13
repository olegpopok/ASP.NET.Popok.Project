using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PL.MVC.Models.Post
{
    public class ChangePostModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [StringLength(512, ErrorMessage = "Maximum length is {1} characters.")]
        [UIHint("Multilinetext")]
        public string Description { get; set; }
    }
}