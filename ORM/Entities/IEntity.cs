using System;

namespace ORM.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
