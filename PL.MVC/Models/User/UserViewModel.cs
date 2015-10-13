using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PL.MVC.Models.User
{
    public class UserViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "Name")]
        public  string Name { get; set; }

        [Display(Name = "Bio")]
        public string Bio { get; set; }

        [Display(Name = "Website")]
        [DataType(DataType.Url)]
        public string Website { get; set; }

        [Display(Name = "CreateDate")]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "posts")]
        public int PostCount { get; set; }
    }
}