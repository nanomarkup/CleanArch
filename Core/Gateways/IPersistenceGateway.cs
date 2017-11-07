using System;
using System.Linq;

namespace Core.Gateways
{
    public interface IPersistenceGateway<Model, DtoModify, DtoQuery> 
        where Model : class
        where DtoModify : class
        where DtoQuery : class
    {
        Guid Add(Model model);
        Model Retrieve(Guid id);
        IQueryable<Model> Retrieve(DtoQuery dto);
        Guid Modify(DtoModify dto);
        Guid Delete(Guid id);
    }
}
