using System;
using System.Linq;

namespace Core.Gateways
{
    public interface IPersistenceGateway<DtoInfo, DtoModify, DtoQuery> 
        where DtoInfo : class
        where DtoModify : class
        where DtoQuery : class
    {
        Guid Add(DtoInfo dto);
        DtoInfo Retrieve(Guid id);
        IQueryable<DtoInfo> Retrieve(DtoQuery dto);
        Guid Modify(DtoModify dto);
        Guid Delete(Guid id);
    }
}
