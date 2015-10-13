using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Interface.DTO;
using ORM.Entities;

namespace DAL.Repositories
{
    public class PhotoRepository<TDalEntity, TOrmEntity> : Repository<TDalEntity, TOrmEntity>
        where TDalEntity: DalEntity where TOrmEntity : class, IEntity
    {
        public PhotoRepository(DbContext context)
            : base(context)
        {}

        public override Guid Create(TDalEntity dalEntity)
        {
            var ormEntity = Mapper.Map<TOrmEntity>(dalEntity);
            _context.Entry(ormEntity).State = EntityState.Added;
            return ormEntity.Id;
        }
    }
}
