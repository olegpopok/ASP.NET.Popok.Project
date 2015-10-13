using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IUserService : IService<BllUser>
    {
        BllUser GetByUserName(string userName);
    }
}
