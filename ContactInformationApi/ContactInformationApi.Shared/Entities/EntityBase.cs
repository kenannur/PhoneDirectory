using System;

namespace ContactInformationApi.Shared.Entities
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
    }

    public abstract class EntityBase : IEntityBase
    {
        public Guid Id { get; set; }
    }
}
