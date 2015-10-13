using System;
using System.Collections.Generic;

namespace DAL.Interface.DTO
{
    public class DalPost : DalEntity
    {
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid AuthorId { get; set; }
        public DalUser Author { get; set; }
        public List<DalLike> Likes { get; set; }
        
    }
}
