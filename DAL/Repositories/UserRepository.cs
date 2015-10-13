using System;
using System.Linq;
using DAL.Interface.DTO;
using ORM.Entities;
using System.Data.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DAL.Interface.Repositories;

namespace DAL.Repositories
{
    public class UserRepository : Repository<DalUser, User>, IUserRepository
    {
        public UserRepository(DbContext context)
        :base(context){}

        public DalUser GetByUserName(string userName)
        {
            var ormUser = _context.Set<User>().Project().To<DalUser>()
                .SingleOrDefault(user => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
            return ormUser;
        }
    }
}
