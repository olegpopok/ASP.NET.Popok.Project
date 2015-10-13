using System;

namespace DAL.Interface.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
