using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PL.MVC.Models.Like;
using PL.MVC.Models.User;

namespace PL.MVC.Models.Post
{
    public class PostViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        public UserViewModel Author { get; set; }

        public string Description { get; set; }

        [UIHint("DateTimeView")]
        public DateTime CreateDate { get; set; }

        public List<ViewLikeModel> Likes { get; set; } 
    }
}