using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.MVC.Models.Like
{
    public class CreateLikeModel
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
    }
}