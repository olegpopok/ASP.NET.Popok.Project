using System;
using System.Collections.Generic;

namespace BLL.Interface.Entities
{
    public class BllPost: BllEntity
    {
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime CreateDate { get; set; }
        public BllUser Author { get; set; }
        public List<BllLike> Likes { get; set; } 
    }
}
