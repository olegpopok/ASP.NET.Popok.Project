using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class BllLike : BllEntity
    {
        public DateTime CreateDate { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public string UserName { get; set; }
    }
}
