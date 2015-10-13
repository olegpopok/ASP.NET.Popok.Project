using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DAL.Mappers;
using ORM.Entities;
using DAL.Interface;
using DAL.Interface.DTO;
using DAL.Interface.Repositories;

namespace DAL.Repositories
{
    public class Repository<TDalEntity, TOrmEntity> : IRepository<TDalEntity>
        where TDalEntity : DalEntity where TOrmEntity : class, IEntity  
    {
        protected readonly DbContext _context;

        static Repository()
        {
            DalEntityMapperConfiguration.Configure();
        }

        public Repository(DbContext context)
        {
            _context = context;
        }

        public virtual Guid Create(TDalEntity dalEntity)
        {
            var id = Guid.NewGuid();
            dalEntity.Id = id;
            var ormEntity = Mapper.Map<TOrmEntity>(dalEntity);
            _context.Entry(ormEntity).State = EntityState.Added;
            return id;
        }

        public void Delete(Guid id)
        {
            var ormEntity = _context.Set<TOrmEntity>().Single(entity => entity.Id == id);
            _context.Set<TOrmEntity>().Remove(ormEntity);
        }

        public void Update(TDalEntity dalEntity)
        {
            var ormEntity = Mapper.Map<TOrmEntity>(dalEntity);
            if (ormEntity != null)
            {
                _context.Set<TOrmEntity>().AddOrUpdate(ormEntity);
            }
        }

        public int Count()
        {
            return _context.Set<TOrmEntity>().Count();
        }

        public int Count(Expression<Func<TDalEntity, bool>> predicate)
        {
            return _context.Set<TOrmEntity>()
                .Where(ExpressionMapper<TDalEntity, TOrmEntity, bool>.Map(predicate))
                .Count();
        }

        public IEnumerable<TDalEntity> GetAll()
        {
            var result = _context.Set<TOrmEntity>().Project().To<TDalEntity>()
                .AsEnumerable();
            return result;
        }

        public IEnumerable<TDalEntity> GetByPredicate(Expression<Func<TDalEntity, bool>> predicate)
        {
            var result = _context.Set<TOrmEntity>()
                .Where(ExpressionMapper<TDalEntity, TOrmEntity, bool>.Map(predicate))
                .Project().To<TDalEntity>().AsEnumerable();
            //Debug.Write(result);
            return result;
        }

        public IEnumerable<TDalEntity> GetByPredicate<TKey>(Expression<Func<TDalEntity, bool>> predicate, Expression<Func<TDalEntity, TKey>> order, int skip, int take)
        {
            var result = _context.Set<TOrmEntity>()
                .Where(ExpressionMapper<TDalEntity, TOrmEntity, bool>.Map(predicate))
                .OrderByDescending(ExpressionMapper<TDalEntity, TOrmEntity, TKey>.Map(order))
                .Skip(skip)
                .Take(take)
                .Project().To<TDalEntity>().AsEnumerable();
            //Debug.Write(result);
            return result;
        }


        public TDalEntity GetById(Guid id)
        {
            var result = _context.Set<TOrmEntity>().Project().To<TDalEntity>()
                .SingleOrDefault(model => model.Id == id);
            //Debug.Write(result);
            return result;
        }
    }
}
