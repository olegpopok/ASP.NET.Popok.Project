using System;

namespace DAL.Interface.DTO
{
    public class DalLike : DalEntity
    {
        public DateTime CreateDate { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public string UserName { get; set; }
    }
}
