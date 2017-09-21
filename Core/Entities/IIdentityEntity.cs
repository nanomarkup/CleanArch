using System;

namespace Core.Entities
{
    public interface IIdentityEntity : IBaseEntity
    {        
        // Primary key, read only  
        Guid Id { get; }
        // Created date, read only
        DateTime Created { get; }
        // Modified date, read only
        DateTime Modified { get; }
        // Uses for creation a new entity
        Guid Create();
        // Uses for initializing/loading entity
        void Initialize(DtoIdentityEntity dto);        
    }

    public class DtoIdentityEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
