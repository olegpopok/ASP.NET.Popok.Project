using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BLL.Interface.Entities;


namespace BLL.Interface.Services
{
    public interface IService<TBllEntity> where TBllEntity : BllEntity
    {
        IEnumerable<TBllEntity> GetAll();
        IEnumerable<TBllEntity> GetByPredicate(Expression<Func<TBllEntity, bool>> predicate);
        IEnumerable<TBllEntity> GetByPredicate<TKey>(Expression<Func<TBllEntity, bool>> predicate, Expression<Func<TBllEntity, TKey>> order, int skip, int take);
        TBllEntity GetById(Guid id);
        int Count();
        int Count(Expression<Func<TBllEntity, bool>> predicate);
        Guid Create(TBllEntity entity);
        void Delete(Guid id);
        void Update(TBllEntity entity);
    }
}
