using DAL.Interface.DTO;

namespace DAL.Interface.Repositories
{
    public interface IUserRepository : IRepository<DalUser>
    {
        DalUser GetByUserName(string userName);
    }
}
