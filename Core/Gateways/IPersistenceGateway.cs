using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Gateways
{
    public interface IPersistenceGateway<DtoAdd, DtoRetrieve, DtoModify, DtoDelete> 
        where DtoAdd : class
        where DtoRetrieve : class
        where DtoModify : class
        where DtoDelete : class
    {
        void Add(DtoAdd dto);
        void Retrieve(DtoRetrieve dto);
        void Modify(DtoModify dto);
        void Delete(DtoDelete dto);
    }
}
