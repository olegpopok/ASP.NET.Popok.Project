using System;
using System.Collections.Generic;
using BLL.Interface.Entities;
using DAL.Interface.Repositories;
using DAL.Interface.DTO;
using  AutoMapper;
using BLL.Interface.Services;

namespace BLL.Services
{
    public class UserService : Service<BllUser, DalUser>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IUserRepository repository)
         :base(unitOfWork, repository){}

        public BllUser GetByUserName(string userName)
        {
            var dalUser = ((IUserRepository) _repository).GetByUserName(userName);
            return Mapper.Map<BllUser>(dalUser);
        }
    }
}
