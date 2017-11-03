using System;

namespace Core.Entities
{      
    public interface IUserEntity<IUserEntityAttrs> : IBaseEntity<IUserEntityAttrs>
        where IUserEntityAttrs : IPoco
    {        
    }
}
