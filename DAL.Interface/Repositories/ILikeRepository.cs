using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interface.Repositories
{
    public interface ILikeRepository : IRepository<DalLike>
    {
        void Delete(Guid postId, Guid userId);
    }
}
