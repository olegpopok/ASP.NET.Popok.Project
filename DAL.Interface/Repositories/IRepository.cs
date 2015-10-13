using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interface.DTO;

namespace DAL.Interface.Repositories
{
    public interface IRepository<TDalEntity> where TDalEntity : DalEntity
    {
        IEnumerable<TDalEntity> GetAll();
        IEnumerable<TDalEntity> GetByPredicate(Expression<Func<TDalEntity, bool>> predicate);
        IEnumerable<TDalEntity> GetByPredicate<TKey>(Expression<Func<TDalEntity, bool>> predicate,
            Expression<Func<TDalEntity, TKey>> order, int skip, int take);
        TDalEntity GetById(Guid id);
        int Count();
        int Count(Expression<Func<TDalEntity, bool>> predicate);
        Guid Create(TDalEntity entity);
        void Delete(Guid Id);
        void Update(TDalEntity entity);
    }
}
