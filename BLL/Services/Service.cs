using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interface.Repositories;
using DAL.Interface.DTO;
using DAL.Interface;
using BLL.Interface.Services;
using BLL.Interface.Entities;
using BLL.Mappers;
using AutoMapper;

namespace BLL.Services
{
    public class Service<TBllEntity, TDalEntity> : IService<TBllEntity>
        where TBllEntity : BllEntity where TDalEntity : DalEntity
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IRepository<TDalEntity> _repository;

        public Service(IUnitOfWork unitOfWork, IRepository<TDalEntity> repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        static Service()
        {
            BllEntityMapperConfiguration.Configure();
        }

        public Guid Create(TBllEntity bllEntity)
        {
            var dalEntity = Mapper.Map<TDalEntity>(bllEntity);
            var id =_repository.Create(dalEntity);
            _unitOfWork.Commit();
            return id;
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
            _unitOfWork.Commit();
        }

        public void Update(TBllEntity bllEntity)
        {
            var dalEntity = Mapper.Map<TDalEntity>(bllEntity);
            _repository.Update(dalEntity);
            _unitOfWork.Commit();
        }

        public IEnumerable<TBllEntity> GetAll()
        {
            var result = _repository.GetAll();
            return Mapper.Map<IEnumerable<TBllEntity>>(result);
        }

        public IEnumerable<TBllEntity> GetByPredicate(Expression<Func<TBllEntity, bool>> predicate)
        {
            var result = _repository.GetByPredicate(ExpressionMapper<TBllEntity, TDalEntity, bool>.Map(predicate));
            return Mapper.Map<IEnumerable<TBllEntity>>(result);
        }

        public IEnumerable<TBllEntity> GetByPredicate<TKey>(Expression<Func<TBllEntity, bool>> predicate, Expression<Func<TBllEntity, TKey>> order, int skip, int take)
        {
            var result = _repository.GetByPredicate(ExpressionMapper<TBllEntity, TDalEntity, bool>.Map(predicate),
                ExpressionMapper<TBllEntity, TDalEntity, TKey>.Map(order), skip, take);
            return Mapper.Map<IEnumerable<TBllEntity>>(result);
        }

        public TBllEntity GetById(Guid id)
        {
            var dalEntity = _repository.GetById(id);
            return Mapper.Map<TBllEntity>(dalEntity);
        }

        public int Count()
        {
            return _repository.Count();
        }

        public int Count(Expression<Func<TBllEntity, bool>> predicate)
        {
            return _repository.Count(ExpressionMapper<TBllEntity, TDalEntity, bool>.Map(predicate));
        }
    }
}
