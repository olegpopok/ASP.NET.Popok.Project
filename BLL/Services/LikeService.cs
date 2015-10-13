using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using DAL.Interface.DTO;
using DAL.Interface.Repositories;

namespace BLL.Services
{
    public class LikeService : Service<BllLike, DalLike>, ILikeService
    {
        public LikeService(IUnitOfWork unitOfWork, ILikeRepository likeRepository)
        :base(unitOfWork, likeRepository){}

        public void Delete(Guid postId, Guid userId)
        {
           ((ILikeRepository)_repository).Delete(postId, userId);
            _unitOfWork.Commit();
        }
    }
}
