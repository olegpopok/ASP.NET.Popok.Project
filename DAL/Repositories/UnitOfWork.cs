using DAL.Interface.Repositories;
using System.Data.Entity;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            this._context = context;
        }

        public void Commit()
        {
            if (_context != null)
            {
                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
