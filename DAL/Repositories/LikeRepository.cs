using System;
using System.Data.Entity;
using System.Linq;
using DAL.Interface.DTO;
using DAL.Interface.Repositories;
using ORM.Entities;

namespace DAL.Repositories
{
    public class LikeRepository : Repository<DalLike, Like>, ILikeRepository
    {
        public LikeRepository(DbContext context)
            :base(context){}

        public void Delete(Guid postId, Guid userId)
        {
            var like = _context.Set<Like>().SingleOrDefault(l => l.UserId == userId && l.PostId == postId);
            _context.Set<Like>().Remove(like);
        }
    }
}
